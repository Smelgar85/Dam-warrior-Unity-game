from flask_sqlalchemy import SQLAlchemy

db = SQLAlchemy()

class Usuario(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    nombre_usuario = db.Column(db.String(80), unique=True, nullable=False)
    contrasena = db.Column(db.String(200), nullable=False)
    partidas = db.relationship('Partida', backref='jugador', lazy=True)
    amistades1 = db.relationship('Amistad', foreign_keys='Amistad.usuario_id1', backref='amigo1', lazy=True)
    amistades2 = db.relationship('Amistad', foreign_keys='Amistad.usuario_id2', backref='amigo2', lazy=True)

class Mapa(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    nombre_mapa = db.Column(db.String(80), unique=True, nullable=False)
    partidas = db.relationship('Partida', backref='mapa', lazy=True)

class Partida(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    usuario_id = db.Column(db.Integer, db.ForeignKey('usuario.id'), nullable=False)
    mapa_id = db.Column(db.Integer, db.ForeignKey('mapa.id'), nullable=False)
    fecha = db.Column(db.DateTime, default=db.func.current_timestamp(), nullable=False)
    puntuacion_destruccion = db.Column(db.Integer, nullable=False)
    estadisticas_precision = db.Column(db.Float, nullable=False)
    tiempo_completado = db.Column(db.Float, nullable=False)
    dano_recibido = db.Column(db.Integer, nullable=False)
    dano_infligido = db.Column(db.Integer, nullable=False)

class Amistad(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    usuario_id1 = db.Column(db.Integer, db.ForeignKey('usuario.id'), nullable=False)
    usuario_id2 = db.Column(db.Integer, db.ForeignKey('usuario.id'), nullable=False)
    fecha_amistad = db.Column(db.DateTime, default=db.func.current_timestamp(), nullable=False)
    __table_args__ = (db.UniqueConstraint('usuario_id1', 'usuario_id2', name='unique_amistad'),
                      db.CheckConstraint('usuario_id1 < usuario_id2', name='check_user_ids'))
