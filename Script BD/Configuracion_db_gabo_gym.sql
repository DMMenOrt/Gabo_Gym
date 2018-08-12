CREATE SCHEMA gym;

CREATE SCHEMA usrs;
ALTER SCHEMA usrs OWNER TO gabo_adm;
ALTER SCHEMA gym OWNER TO gabo_adm;

SET search_path = gym, pg_catalog;

drop table if exists precios;
CREATE TABLE precios (
    clave_producto SERIAL NOT NULL,
    fecha_alta date NOT NULL,
    precio decimal(6,2),
    fecha_expiracion date
);

drop table if exists productos;
CREATE TABLE productos (
    clave_producto SERIAL NOT NULL,
    clave_tipo_producto integer,
    nombre_producto character varying(40),
    duracion integer
);

drop table if exists simetrias;
CREATE TABLE simetrias (
    clave_socio integer NOT NULL,
    fecha date,
    estatura numeric(5,2),
    peso numeric(5,2),
    pecho numeric(5,2),
    espalda numeric(5,2),
    biceps numeric(5,2),
    cintura numeric(5,2),
    cadera numeric(5,2),
    cuadriceps numeric(5,2),
    pantorrilla numeric(5,2),
    agua_espalda_sup numeric(5,2),
    grasa_espalda_sup numeric(5,2),
    agua_espalda_inf numeric(5,2),
    grasa_espalda_inf numeric(5,2),
    agua_abdomen_sup numeric(5,2),
    grasa_abdomen_sup numeric(5,2),
    agua_abdomen_inf numeric(5,2),
    grasa_abdomen_inf numeric(5,2),
    agua_triceps numeric(5,2),
    grasa_triceps numeric(5,2),
    agua_aductores numeric(5,2),
    grasa_aductores numeric(5,2),
    agua_chaparreras numeric(5,2),
    grasa_chaparreras numeric(5,2)
);

drop table if exists socios;
CREATE TABLE socios (
    clave_socio SERIAL NOT NULL,
    nombre character varying(40),
    primer_apellido character varying(20),
    segundo_apellido character varying(20),
    fecha_inicio date,
    fecha_fin date
);

drop table if exists tipo_producto;
CREATE TABLE tipo_producto (
    clave_tipo_producto SERIAL NOT NULL,
    tipo_producto character varying(20)
);

drop table if exists tipos_ventas;
CREATE TABLE tipos_ventas (
    clave_tipo_venta SERIAL NOT NULL,
    tipo_venta character varying(30)
);

drop table if exists venta_producto;
CREATE TABLE venta_producto (
    clave_producto integer NOT NULL,
    clave_venta integer NOT NULL,
    precio_producto decimal(6,2),
    cantidad integer
);

alter table venta_producto alter column cantidad set default 1;

drop table if exists ventas;
CREATE TABLE ventas (
    clave_venta SERIAL NOT NULL,
    clave_tipo_venta integer,
    clave_socio integer,
    fecha_venta date
);

SET search_path = usrs, pg_catalog;

drop table if exists tipo_usuarios;
CREATE TABLE tipo_usuarios (
    id_tipo_usuario SERIAL NOT NULL,
    tipo_usuario character varying(40),
    gestor_usuarios boolean,
    gestor_socios boolean,
    gestor_productos boolean,
    gestor_empleados boolean
);

drop table if exists usuarios;
CREATE TABLE usuarios (
    id_usuario SERIAL NOT NULL,
    nombre_usuario character varying(40),
    nombre character varying(40),
    primer_apellido character varying(40),
    segundo_apellido character varying(40),
    id_tipo_usuario integer
);

SET search_path = gym, pg_catalog;

ALTER TABLE ONLY precios
    ADD CONSTRAINT precios_pkey PRIMARY KEY (clave_producto, fecha_alta);

ALTER TABLE ONLY productos
    ADD CONSTRAINT productos_pkey PRIMARY KEY (clave_producto);

ALTER TABLE ONLY simetrias
    ADD CONSTRAINT simetrias_pkey PRIMARY KEY (clave_socio);

ALTER TABLE ONLY socios
    ADD CONSTRAINT socios_pkey PRIMARY KEY (clave_socio);

ALTER TABLE ONLY tipo_producto
    ADD CONSTRAINT tipo_producto_pkey PRIMARY KEY (clave_tipo_producto);

ALTER TABLE ONLY tipos_ventas
    ADD CONSTRAINT tipos_ventas_pkey PRIMARY KEY (clave_tipo_venta);

ALTER TABLE ONLY venta_producto
    ADD CONSTRAINT venta_producto_pkey PRIMARY KEY (clave_producto, clave_venta);

ALTER TABLE ONLY ventas
    ADD CONSTRAINT ventas_pkey PRIMARY KEY (clave_venta);

SET search_path = usrs, pg_catalog;

ALTER TABLE ONLY tipo_usuarios
    ADD CONSTRAINT tipo_usuarios_pkey PRIMARY KEY (id_tipo_usuario);

ALTER TABLE ONLY usuarios
    ADD CONSTRAINT usuarios_pkey PRIMARY KEY (id_usuario);

SET search_path = gym, pg_catalog;

ALTER TABLE ONLY precios
    ADD CONSTRAINT precios_clave_producto_fkey FOREIGN KEY (clave_producto) REFERENCES productos(clave_producto) ON DELETE CASCADE;

ALTER TABLE ONLY productos
    ADD CONSTRAINT productos_clave_tipo_producto_fkey FOREIGN KEY (clave_tipo_producto) REFERENCES tipo_producto(clave_tipo_producto) ON DELETE CASCADE;

ALTER TABLE ONLY venta_producto
    ADD CONSTRAINT venta_producto_clave_producto_fkey FOREIGN KEY (clave_producto) REFERENCES productos(clave_producto) ON DELETE CASCADE;

ALTER TABLE ONLY venta_producto
    ADD CONSTRAINT venta_producto_clave_venta_fkey FOREIGN KEY (clave_venta) REFERENCES ventas(clave_venta) ON DELETE CASCADE;

ALTER TABLE ONLY ventas
    ADD CONSTRAINT ventas_clave_socio_fkey FOREIGN KEY (clave_socio) REFERENCES socios(clave_socio) ON DELETE CASCADE;

SET search_path = usrs, pg_catalog;

ALTER TABLE ONLY usuarios
    ADD CONSTRAINT usuarios_id_tipo_usuario_fkey FOREIGN KEY (id_tipo_usuario) REFERENCES tipo_usuarios(id_tipo_usuario) ON DELETE CASCADE;

INSERT INTO gym.tipo_producto values ('1','Producto');
INSERT INTO gym.tipo_producto VALUES('2','Servicio');

INSERT INTO gym.tipos_ventas values('1','Suscripcion a servicio');
INSERT INTO gym.tipos_ventas VALUES('2','Pago de servicio');
INSERT INTO gym.tipos_ventas VALUES('3','Compra de producto');
INSERT INTO gym.tipos_ventas VALUES('4','Visita');

INSERT INTO usrs.tipo_usuarios VALUES ('1', 'Administrador');
INSERT INTO usrs.tipo_usuarios VALUES ('2', 'Empleado');
