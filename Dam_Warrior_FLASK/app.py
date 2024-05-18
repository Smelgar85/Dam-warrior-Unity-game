from flask import Flask, render_template, request, redirect, url_for, session
from werkzeug.security import generate_password_hash, check_password_hash
from models import db, Usuario, Mapa, Partida, Amistad
from flask_migrate import Migrate

app = Flask(__name__)
app.config['SECRET_KEY'] = 'your_secret_key'
app.config['SQLALCHEMY_DATABASE_URI'] = 'mysql+mysqldb://Smelgar85:RE1P+QPp@Smelgar85.mysql.eu.pythonanywhere-services.com:3306/Smelgar85$DAMWARRIOR'
db.init_app(app)
migrate = Migrate(app, db)

@app.route('/')
def home():
    return redirect(url_for('login'))

@app.route('/register', methods=['GET', 'POST'])
def register():
    if request.method == 'POST':
        nombre_usuario = request.form['nombre_usuario']
        contrasena = request.form['contrasena']
        hashed_password = generate_password_hash(contrasena, method='sha256')
        nuevo_usuario = Usuario(nombre_usuario=nombre_usuario, contrasena=hashed_password)
        db.session.add(nuevo_usuario)
        db.session.commit()
        return redirect(url_for('login'))
    return render_template('register.html')

@app.route('/login', methods=['GET', 'POST'])
def login():
    if request.method == 'POST':
        nombre_usuario = request.form['nombre_usuario']
        contrasena = request.form['contrasena']
        usuario = Usuario.query.filter_by(nombre_usuario=nombre_usuario).first()
        if usuario and check_password_hash(usuario.contrasena, contrasena):
            session['usuario_id'] = usuario.id
            return redirect(url_for('dashboard'))
        else:
            return 'Login fallido'
    return render_template('login.html')

@app.route('/dashboard')
def dashboard():
    if 'usuario_id' not in session:
        return redirect(url_for('login'))
    usuario = Usuario.query.filter_by(id=session['usuario_id']).first()
    return render_template('dashboard.html', nombre_usuario=usuario.nombre_usuario)

if __name__ == '__main__':
    app.run(debug=True)
