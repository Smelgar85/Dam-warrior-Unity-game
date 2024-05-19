from flask import Flask, render_template, request, redirect, url_for, session, flash
from werkzeug.security import generate_password_hash, check_password_hash
from models import db, Usuario, Mapa, Partida, Amistad
from flask_migrate import Migrate
from sqlalchemy.exc import OperationalError

app = Flask(__name__)
app.config['SECRET_KEY'] = 'your_secret_key'
app.config['SQLALCHEMY_DATABASE_URI'] = 'mysql+mysqldb://Smelgar85:RE1P+QPp@Smelgar85.mysql.eu.pythonanywhere-services.com:3306/Smelgar85$DAMWARRIOR'
app.config['SQLALCHEMY_ENGINE_OPTIONS'] = {
    'pool_pre_ping': True,  # Esto permite verificar la conexión antes de usarla
    'pool_recycle': 280  # Reciclar las conexiones para evitar desconexiones por timeout
}
db.init_app(app)
migrate = Migrate(app, db)

@app.route('/')
def home():
    return redirect(url_for('login'))

@app.route('/register', methods=['GET', 'POST'])
def register():
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

        # Método correcto para generar el hash de la contraseña
        hashed_password = generate_password_hash(contrasena, method='pbkdf2:sha256')
        nuevo_usuario = Usuario(nombre_usuario=nombre_usuario, email=email, contrasena=hashed_password)
        db.session.add(nuevo_usuario)
        db.session.commit()

        flash('Usuario creado correctamente. Por favor, inicia sesión.')
        return redirect(url_for('login'))
    return render_template('register.html')

@app.route('/login', methods=['GET', 'POST'])
def login():
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
    if 'usuario_id' not in session:
        return redirect(url_for('login'))

    usuario = Usuario.query.filter_by(id=session['usuario_id']).first()
    # Obtener las mejores partidas ordenadas por puntuación
    mejores_partidas = Partida.query.order_by(Partida.puntuacion_destruccion.desc()).all()

    return render_template('dashboard.html', nombre_usuario=usuario.nombre_usuario, mejores_partidas=mejores_partidas)

@app.route('/partidas')
def partidas():
    if 'usuario_id' not in session:
        return redirect(url_for('login'))

    # Lógica para obtener las partidas (puedes ajustar esto según tus necesidades)
    usuario = Usuario.query.filter_by(id=session['usuario_id']).first()
    partidas = Partida.query.filter_by(usuario_id=usuario.id).all()

    return render_template('partidas.html', partidas=partidas)

@app.route('/amigos')
def amigos():
    if 'usuario_id' not in session:
        return redirect(url_for('login'))

    # Lógica para obtener los amigos (puedes ajustar esto según tus necesidades)
    usuario = Usuario.query.filter_by(id=session['usuario_id']).first()
    amigos = usuario.amistades1 + usuario.amistades2

    return render_template('amigos.html', amigos=amigos)

@app.route('/logout')
def logout():
    session.pop('usuario_id', None)
    return redirect(url_for('login'))


if __name__ == '__main__':
    app.run(debug=True)
