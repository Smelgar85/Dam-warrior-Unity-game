from flask import Flask, render_template, request, redirect, url_for, session, flash, jsonify
from werkzeug.security import generate_password_hash, check_password_hash
from models import db, Usuario, Mapa, Partida, Amistad
from flask_migrate import Migrate
from sqlalchemy.exc import OperationalError
import datetime
import json

# Crear una instancia de la aplicación Flask
app = Flask(__name__)
app.config['SECRET_KEY'] = 'your_secret_key'  # Clave secreta para la sesión
app.config['SQLALCHEMY_DATABASE_URI'] = 'mysql+mysqldb://Smelgar85:RE1P+QPp@Smelgar85.mysql.eu.pythonanywhere-services.com:3306/Smelgar85$DAMWARRIOR'
app.config['SQLALCHEMY_ENGINE_OPTIONS'] = {
    'pool_pre_ping': True,  # Verifica la conexión antes de usarla
    'pool_recycle': 280  # Recicla las conexiones para evitar desconexiones por timeout
}
db.init_app(app)
migrate = Migrate(app, db)  # Configura la migración de la base de datos

@app.route('/jugar')
def jugar():
    # Verifica si el usuario ha iniciado sesión
    if 'usuario_id' not in session:
        return redirect(url_for('login'))
    return render_template('jugar.html')  # Muestra la página para jugar

@app.route('/')
def home():
    # Redirige a la página de inicio o al dashboard si el usuario ha iniciado sesión
    if 'usuario_id' in session:
        return redirect(url_for('dashboard'))
    return redirect(url_for('login'))

@app.route('/usuario/<int:usuario_id>')
def perfil_usuario(usuario_id):
    # Muestra el perfil de un usuario específico
    if 'usuario_id' not in session:
        return redirect(url_for('login'))

    usuario = Usuario.query.get(usuario_id)
    if not usuario:
        flash('Usuario no encontrado.')
        return redirect(url_for('dashboard'))

    partidas = Partida.query.filter_by(usuario_id=usuario_id).order_by(Partida.fecha.desc()).all()

    return render_template('perfil_usuario.html', usuario=usuario, partidas=partidas)

@app.route('/register', methods=['GET', 'POST'])
def register():
    # Gestión del registro de nuevos usuarios
    if request.method == 'POST':
        nombre_usuario = request.form['nombre_usuario']
        email = request.form['email']
        contrasena = request.form['contrasena']

        # Verificar si el nombre de usuario o el correo electrónico ya existen
        usuario_existente = Usuario.query.filter_by(nombre_usuario=nombre_usuario).first()
        email_existente = Usuario.query.filter_by(email=email).first()

        if usuario_existente:
            flash('El nombre de usuario ya está en uso. Por favor, elige otro.')
            return redirect(url_for('register'))

        if email_existente:
            flash('El correo electrónico ya está en uso. Por favor, elige otro.')
            return redirect(url_for('register'))

        # Generar el hash de la contraseña
        hashed_password = generate_password_hash(contrasena, method='pbkdf2:sha256')
        nuevo_usuario = Usuario(nombre_usuario=nombre_usuario, email=email, contrasena=hashed_password)
        db.session.add(nuevo_usuario)
        db.session.commit()

        flash('Usuario creado correctamente. Por favor, inicia sesión.')
        return redirect(url_for('login'))
    return render_template('register.html')

@app.route('/login', methods=['GET', 'POST'])
def login():
    # Gestión del inicio de sesión de los usuarios
    if request.method == 'POST':
        nombre_usuario = request.form['nombre_usuario']
        contrasena = request.form['contrasena']
        try:
            usuario = Usuario.query.filter_by(nombre_usuario=nombre_usuario).first()
            if usuario and check_password_hash(usuario.contrasena, contrasena):
                session['usuario_id'] = usuario.id
                return redirect(url_for('dashboard'))
            else:
                flash('Nombre de usuario o contraseña incorrectos.')
        except OperationalError:
            flash('Error de conexión con la base de datos. Por favor, inténtelo de nuevo.')
            return redirect(url_for('login'))
    return render_template('login.html')

@app.route('/dashboard')
def dashboard():
    # Muestra el panel principal del usuario con sus mejores partidas
    if 'usuario_id' not in session:
        return redirect(url_for('login'))

    usuario = Usuario.query.filter_by(id=session['usuario_id']).first()
    mejores_partidas = Partida.query.order_by(Partida.puntuacion_destruccion.desc()).all()

    return render_template('dashboard.html', nombre_usuario=usuario.nombre_usuario, mejores_partidas=mejores_partidas)

@app.route('/partidas')
def partidas():
    # Muestra las partidas del usuario
    if 'usuario_id' not in session:
        return redirect(url_for('login'))

    usuario = Usuario.query.filter_by(id=session['usuario_id']).first()
    partidas = Partida.query.filter_by(usuario_id=usuario.id).all()

    return render_template('partidas.html', partidas=partidas)

@app.route('/amigos', methods=['GET', 'POST'])
def amigos():
    # Gestión de amigos del usuario
    if 'usuario_id' not in session:
        return redirect(url_for('login'))

    usuario = Usuario.query.get(session['usuario_id'])
    if request.method == 'POST':
        if 'buscar_amigo' in request.form:
            nombre_usuario = request.form['nombre_usuario']
            amigo = Usuario.query.filter_by(nombre_usuario=nombre_usuario).first()
            if amigo:
                nueva_amistad = Amistad(usuario_id1=min(usuario.id, amigo.id), usuario_id2=max(usuario.id, amigo.id))
                db.session.add(nueva_amistad)
                db.session.commit()
                flash('Amigo agregado correctamente.')
            else:
                flash('Usuario no encontrado.')
        elif 'eliminar_amigo' in request.form:
            amigo_id = request.form['amigo_id']
            Amistad.query.filter(
                (Amistad.usuario_id1 == usuario.id) & (Amistad.usuario_id2 == amigo_id) |
                (Amistad.usuario_id1 == amigo_id) & (Amistad.usuario_id2 == usuario.id)
            ).delete()
            db.session.commit()
            flash('Amigo eliminado correctamente.')

    amistades = Amistad.query.filter(
        (Amistad.usuario_id1 == usuario.id) | (Amistad.usuario_id2 == usuario.id)
    ).all()

    amigos = []
    for amistad in amistades:
        if amistad.usuario_id1 == usuario.id:
            amigos.append(Usuario.query.get(amistad.usuario_id2))
        else:
            amigos.append(Usuario.query.get(amistad.usuario_id1))

    return render_template('amigos.html', usuario=usuario, amigos=amigos)

@app.route('/logout')
def logout():
    # Cierra la sesión del usuario
    session.pop('usuario_id', None)
    return redirect(url_for('login'))

@app.route('/perfil', methods=['GET', 'POST'])
def perfil():
    # Gestión del perfil del usuario
    if 'usuario_id' not in session:
        return redirect(url_for('login'))

    usuario = Usuario.query.filter_by(id=session['usuario_id']).first()

    if request.method == 'POST':
        nombre_usuario = request.form.get('nombre_usuario')
        email = request.form.get('email')
        accion = request.form.get('accion')

        if accion == 'Actualizar':
            usuario_existente = Usuario.query.filter_by(nombre_usuario=nombre_usuario).first()
            email_existente = Usuario.query.filter_by(email=email).first()

            if usuario_existente and usuario_existente.id != usuario.id:
                flash('El nombre de usuario ya está en uso. Por favor, elige otro.')
            elif email_existente and email_existente.id != usuario.id:
                flash('El correo electrónico ya está en uso. Por favor, elige otro.')
            else:
                usuario.nombre_usuario = nombre_usuario
                usuario.email = email
                db.session.commit()
                flash('Perfil actualizado correctamente.')
        elif accion == 'Borrar Partidas':
            Partida.query.filter_by(usuario_id=usuario.id).delete()
            db.session.commit()
            flash('Todas tus partidas han sido borradas.')
        elif accion == 'Borrar Cuenta':
            Partida.query.filter_by(usuario_id=usuario.id).delete()
            Amistad.query.filter((Amistad.usuario_id1 == usuario.id) | (Amistad.usuario_id2 == usuario.id)).delete()
            db.session.delete(usuario)
            db.session.commit()
            session.pop('usuario_id', None)
            flash('Tu cuenta ha sido borrada.')
            return redirect(url_for('register'))

    return render_template('perfil.html', usuario=usuario)

@app.route('/guardar_estadisticas', methods=['POST'])
def guardar_estadisticas():
    # Guarda las estadísticas de una partida enviada por una aplicación externa
    username = request.form['username']
    password = request.form['password']
    stats_json = request.form['stats']

    user = Usuario.query.filter_by(nombre_usuario=username).first()
    if not user or not check_password_hash(user.contrasena, password):
        return jsonify({'status': 'error', 'message': 'Usuario o contraseña incorrectos'}), 401

    stats = json.loads(stats_json)

    partida = Partida(
        usuario_id=user.id,
        mapa_id=1,  # Asumiendo que "Map 1" tiene ID 1
        fecha=datetime.datetime.strptime(stats['fecha'], '%Y-%m-%d %H:%M:%S'),
        puntuacion_destruccion=stats['puntuacion'],
        estadisticas_precision=stats['precision'] * 100,
        tiempo_completado=stats['tiempoCompletado'],
        dano_recibido=stats['danoRecibido'],
        dano_infligido=stats['danoCausado']
    )

    db.session.add(partida)
    db.session.commit()

    return jsonify({'status': 'success', 'message': 'Estadísticas guardadas correctamente'})

if __name__ == '__main__':
    app.run(debug=True)
