--
-- PostgreSQL database dump
--

-- Dumped from database version 10.5
-- Dumped by pg_dump version 10.5

-- Started on 2018-08-13 01:21:39

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

DROP DATABASE gabo_gym;
--
-- TOC entry 2899 (class 1262 OID 16393)
-- Name: gabo_gym; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE gabo_gym WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'Spanish_Mexico.1252' LC_CTYPE = 'Spanish_Mexico.1252';


ALTER DATABASE gabo_gym OWNER TO postgres;

\connect gabo_gym

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 9 (class 2615 OID 16396)
-- Name: gym; Type: SCHEMA; Schema: -; Owner: gabo_adm
--

CREATE SCHEMA gym;


ALTER SCHEMA gym OWNER TO gabo_adm;

--
-- TOC entry 8 (class 2615 OID 16397)
-- Name: usrs; Type: SCHEMA; Schema: -; Owner: gabo_adm
--

CREATE SCHEMA usrs;


ALTER SCHEMA usrs OWNER TO gabo_adm;

--
-- TOC entry 1 (class 3079 OID 12924)
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- TOC entry 2902 (class 0 OID 0)
-- Dependencies: 1
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 199 (class 1259 OID 16400)
-- Name: precios; Type: TABLE; Schema: gym; Owner: postgres
--

CREATE TABLE gym.precios (
    clave_producto integer NOT NULL,
    fecha_alta date NOT NULL,
    precio numeric(6,2),
    fecha_expiracion date
);


ALTER TABLE gym.precios OWNER TO postgres;

--
-- TOC entry 198 (class 1259 OID 16398)
-- Name: precios_clave_producto_seq; Type: SEQUENCE; Schema: gym; Owner: postgres
--

CREATE SEQUENCE gym.precios_clave_producto_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE gym.precios_clave_producto_seq OWNER TO postgres;

--
-- TOC entry 2903 (class 0 OID 0)
-- Dependencies: 198
-- Name: precios_clave_producto_seq; Type: SEQUENCE OWNED BY; Schema: gym; Owner: postgres
--

ALTER SEQUENCE gym.precios_clave_producto_seq OWNED BY gym.precios.clave_producto;


--
-- TOC entry 201 (class 1259 OID 16406)
-- Name: productos; Type: TABLE; Schema: gym; Owner: postgres
--

CREATE TABLE gym.productos (
    clave_producto integer NOT NULL,
    clave_tipo_producto integer,
    nombre_producto character varying(40),
    duracion integer
);


ALTER TABLE gym.productos OWNER TO postgres;

--
-- TOC entry 200 (class 1259 OID 16404)
-- Name: productos_clave_producto_seq; Type: SEQUENCE; Schema: gym; Owner: postgres
--

CREATE SEQUENCE gym.productos_clave_producto_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE gym.productos_clave_producto_seq OWNER TO postgres;

--
-- TOC entry 2904 (class 0 OID 0)
-- Dependencies: 200
-- Name: productos_clave_producto_seq; Type: SEQUENCE OWNED BY; Schema: gym; Owner: postgres
--

ALTER SEQUENCE gym.productos_clave_producto_seq OWNED BY gym.productos.clave_producto;


--
-- TOC entry 202 (class 1259 OID 16410)
-- Name: simetrias; Type: TABLE; Schema: gym; Owner: postgres
--

CREATE TABLE gym.simetrias (
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


ALTER TABLE gym.simetrias OWNER TO postgres;

--
-- TOC entry 204 (class 1259 OID 16415)
-- Name: socios; Type: TABLE; Schema: gym; Owner: postgres
--

CREATE TABLE gym.socios (
    clave_socio integer NOT NULL,
    nombre character varying(40),
    primer_apellido character varying(20),
    segundo_apellido character varying(20),
    fecha_inicio date,
    fecha_fin date
);


ALTER TABLE gym.socios OWNER TO postgres;

--
-- TOC entry 203 (class 1259 OID 16413)
-- Name: socios_clave_socio_seq; Type: SEQUENCE; Schema: gym; Owner: postgres
--

CREATE SEQUENCE gym.socios_clave_socio_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE gym.socios_clave_socio_seq OWNER TO postgres;

--
-- TOC entry 2905 (class 0 OID 0)
-- Dependencies: 203
-- Name: socios_clave_socio_seq; Type: SEQUENCE OWNED BY; Schema: gym; Owner: postgres
--

ALTER SEQUENCE gym.socios_clave_socio_seq OWNED BY gym.socios.clave_socio;


--
-- TOC entry 213 (class 1259 OID 16507)
-- Name: venta_producto; Type: TABLE; Schema: gym; Owner: postgres
--

CREATE TABLE gym.venta_producto (
    clave_producto integer NOT NULL,
    clave_venta integer NOT NULL,
    clave_tipo_venta integer,
    precio_producto numeric(6,2),
    cantidad integer DEFAULT 1
);


ALTER TABLE gym.venta_producto OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 16517)
-- Name: suma_venta; Type: VIEW; Schema: gym; Owner: postgres
--

CREATE VIEW gym.suma_venta AS
 SELECT sum(vp.precio_producto) AS sum,
    vp.clave_venta
   FROM gym.venta_producto vp
  GROUP BY vp.clave_venta;


ALTER TABLE gym.suma_venta OWNER TO postgres;

--
-- TOC entry 206 (class 1259 OID 16421)
-- Name: tipo_producto; Type: TABLE; Schema: gym; Owner: postgres
--

CREATE TABLE gym.tipo_producto (
    clave_tipo_producto integer NOT NULL,
    tipo_producto character varying(20)
);


ALTER TABLE gym.tipo_producto OWNER TO postgres;

--
-- TOC entry 205 (class 1259 OID 16419)
-- Name: tipo_producto_clave_tipo_producto_seq; Type: SEQUENCE; Schema: gym; Owner: postgres
--

CREATE SEQUENCE gym.tipo_producto_clave_tipo_producto_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE gym.tipo_producto_clave_tipo_producto_seq OWNER TO postgres;

--
-- TOC entry 2906 (class 0 OID 0)
-- Dependencies: 205
-- Name: tipo_producto_clave_tipo_producto_seq; Type: SEQUENCE OWNED BY; Schema: gym; Owner: postgres
--

ALTER SEQUENCE gym.tipo_producto_clave_tipo_producto_seq OWNED BY gym.tipo_producto.clave_tipo_producto;


--
-- TOC entry 208 (class 1259 OID 16427)
-- Name: tipos_ventas; Type: TABLE; Schema: gym; Owner: postgres
--

CREATE TABLE gym.tipos_ventas (
    clave_tipo_venta integer NOT NULL,
    tipo_venta character varying(30)
);


ALTER TABLE gym.tipos_ventas OWNER TO postgres;

--
-- TOC entry 207 (class 1259 OID 16425)
-- Name: tipos_ventas_clave_tipo_venta_seq; Type: SEQUENCE; Schema: gym; Owner: postgres
--

CREATE SEQUENCE gym.tipos_ventas_clave_tipo_venta_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE gym.tipos_ventas_clave_tipo_venta_seq OWNER TO postgres;

--
-- TOC entry 2907 (class 0 OID 0)
-- Dependencies: 207
-- Name: tipos_ventas_clave_tipo_venta_seq; Type: SEQUENCE OWNED BY; Schema: gym; Owner: postgres
--

ALTER SEQUENCE gym.tipos_ventas_clave_tipo_venta_seq OWNED BY gym.tipos_ventas.clave_tipo_venta;


--
-- TOC entry 215 (class 1259 OID 16513)
-- Name: ventas; Type: TABLE; Schema: gym; Owner: postgres
--

CREATE TABLE gym.ventas (
    clave_venta integer NOT NULL,
    clave_socio integer,
    fecha_venta date
);


ALTER TABLE gym.ventas OWNER TO postgres;

--
-- TOC entry 214 (class 1259 OID 16511)
-- Name: ventas_clave_venta_seq; Type: SEQUENCE; Schema: gym; Owner: postgres
--

CREATE SEQUENCE gym.ventas_clave_venta_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE gym.ventas_clave_venta_seq OWNER TO postgres;

--
-- TOC entry 2908 (class 0 OID 0)
-- Dependencies: 214
-- Name: ventas_clave_venta_seq; Type: SEQUENCE OWNED BY; Schema: gym; Owner: postgres
--

ALTER SEQUENCE gym.ventas_clave_venta_seq OWNED BY gym.ventas.clave_venta;


--
-- TOC entry 210 (class 1259 OID 16442)
-- Name: tipo_usuarios; Type: TABLE; Schema: usrs; Owner: postgres
--

CREATE TABLE usrs.tipo_usuarios (
    id_tipo_usuario integer NOT NULL,
    tipo_usuario character varying(40),
    gestor_usuarios boolean,
    gestor_socios boolean,
    gestor_productos boolean,
    gestor_empleados boolean
);


ALTER TABLE usrs.tipo_usuarios OWNER TO postgres;

--
-- TOC entry 209 (class 1259 OID 16440)
-- Name: tipo_usuarios_id_tipo_usuario_seq; Type: SEQUENCE; Schema: usrs; Owner: postgres
--

CREATE SEQUENCE usrs.tipo_usuarios_id_tipo_usuario_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE usrs.tipo_usuarios_id_tipo_usuario_seq OWNER TO postgres;

--
-- TOC entry 2909 (class 0 OID 0)
-- Dependencies: 209
-- Name: tipo_usuarios_id_tipo_usuario_seq; Type: SEQUENCE OWNED BY; Schema: usrs; Owner: postgres
--

ALTER SEQUENCE usrs.tipo_usuarios_id_tipo_usuario_seq OWNED BY usrs.tipo_usuarios.id_tipo_usuario;


--
-- TOC entry 212 (class 1259 OID 16448)
-- Name: usuarios; Type: TABLE; Schema: usrs; Owner: postgres
--

CREATE TABLE usrs.usuarios (
    id_usuario integer NOT NULL,
    nombre_usuario character varying(40),
    nombre character varying(40),
    primer_apellido character varying(40),
    segundo_apellido character varying(40),
    id_tipo_usuario integer
);


ALTER TABLE usrs.usuarios OWNER TO postgres;

--
-- TOC entry 211 (class 1259 OID 16446)
-- Name: usuarios_id_usuario_seq; Type: SEQUENCE; Schema: usrs; Owner: postgres
--

CREATE SEQUENCE usrs.usuarios_id_usuario_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE usrs.usuarios_id_usuario_seq OWNER TO postgres;

--
-- TOC entry 2910 (class 0 OID 0)
-- Dependencies: 211
-- Name: usuarios_id_usuario_seq; Type: SEQUENCE OWNED BY; Schema: usrs; Owner: postgres
--

ALTER SEQUENCE usrs.usuarios_id_usuario_seq OWNED BY usrs.usuarios.id_usuario;


--
-- TOC entry 2726 (class 2604 OID 16403)
-- Name: precios clave_producto; Type: DEFAULT; Schema: gym; Owner: postgres
--

ALTER TABLE ONLY gym.precios ALTER COLUMN clave_producto SET DEFAULT nextval('gym.precios_clave_producto_seq'::regclass);


--
-- TOC entry 2727 (class 2604 OID 16409)
-- Name: productos clave_producto; Type: DEFAULT; Schema: gym; Owner: postgres
--

ALTER TABLE ONLY gym.productos ALTER COLUMN clave_producto SET DEFAULT nextval('gym.productos_clave_producto_seq'::regclass);


--
-- TOC entry 2728 (class 2604 OID 16418)
-- Name: socios clave_socio; Type: DEFAULT; Schema: gym; Owner: postgres
--

ALTER TABLE ONLY gym.socios ALTER COLUMN clave_socio SET DEFAULT nextval('gym.socios_clave_socio_seq'::regclass);


--
-- TOC entry 2729 (class 2604 OID 16424)
-- Name: tipo_producto clave_tipo_producto; Type: DEFAULT; Schema: gym; Owner: postgres
--

ALTER TABLE ONLY gym.tipo_producto ALTER COLUMN clave_tipo_producto SET DEFAULT nextval('gym.tipo_producto_clave_tipo_producto_seq'::regclass);


--
-- TOC entry 2730 (class 2604 OID 16430)
-- Name: tipos_ventas clave_tipo_venta; Type: DEFAULT; Schema: gym; Owner: postgres
--

ALTER TABLE ONLY gym.tipos_ventas ALTER COLUMN clave_tipo_venta SET DEFAULT nextval('gym.tipos_ventas_clave_tipo_venta_seq'::regclass);


--
-- TOC entry 2734 (class 2604 OID 16516)
-- Name: ventas clave_venta; Type: DEFAULT; Schema: gym; Owner: postgres
--

ALTER TABLE ONLY gym.ventas ALTER COLUMN clave_venta SET DEFAULT nextval('gym.ventas_clave_venta_seq'::regclass);


--
-- TOC entry 2731 (class 2604 OID 16445)
-- Name: tipo_usuarios id_tipo_usuario; Type: DEFAULT; Schema: usrs; Owner: postgres
--

ALTER TABLE ONLY usrs.tipo_usuarios ALTER COLUMN id_tipo_usuario SET DEFAULT nextval('usrs.tipo_usuarios_id_tipo_usuario_seq'::regclass);


--
-- TOC entry 2732 (class 2604 OID 16451)
-- Name: usuarios id_usuario; Type: DEFAULT; Schema: usrs; Owner: postgres
--

ALTER TABLE ONLY usrs.usuarios ALTER COLUMN id_usuario SET DEFAULT nextval('usrs.usuarios_id_usuario_seq'::regclass);


--
-- TOC entry 2877 (class 0 OID 16400)
-- Dependencies: 199
-- Data for Name: precios; Type: TABLE DATA; Schema: gym; Owner: postgres
--

INSERT INTO gym.precios (clave_producto, fecha_alta, precio, fecha_expiracion) VALUES (1, '2018-08-12', 50.00, NULL);
INSERT INTO gym.precios (clave_producto, fecha_alta, precio, fecha_expiracion) VALUES (2, '2018-08-12', 690.00, NULL);
INSERT INTO gym.precios (clave_producto, fecha_alta, precio, fecha_expiracion) VALUES (3, '2018-08-12', 1000.00, NULL);
INSERT INTO gym.precios (clave_producto, fecha_alta, precio, fecha_expiracion) VALUES (4, '2018-08-12', 500.00, NULL);


--
-- TOC entry 2879 (class 0 OID 16406)
-- Dependencies: 201
-- Data for Name: productos; Type: TABLE DATA; Schema: gym; Owner: postgres
--

INSERT INTO gym.productos (clave_producto, clave_tipo_producto, nombre_producto, duracion) VALUES (1, 1, 'Visita', 0);
INSERT INTO gym.productos (clave_producto, clave_tipo_producto, nombre_producto, duracion) VALUES (2, 2, 'Gym', 1);
INSERT INTO gym.productos (clave_producto, clave_tipo_producto, nombre_producto, duracion) VALUES (3, 2, 'Gym', 2);
INSERT INTO gym.productos (clave_producto, clave_tipo_producto, nombre_producto, duracion) VALUES (4, 1, 'Proteina', 0);


--
-- TOC entry 2880 (class 0 OID 16410)
-- Dependencies: 202
-- Data for Name: simetrias; Type: TABLE DATA; Schema: gym; Owner: postgres
--



--
-- TOC entry 2882 (class 0 OID 16415)
-- Dependencies: 204
-- Data for Name: socios; Type: TABLE DATA; Schema: gym; Owner: postgres
--

INSERT INTO gym.socios (clave_socio, nombre, primer_apellido, segundo_apellido, fecha_inicio, fecha_fin) VALUES (1, 'Duego Mogol', 'Meneses', 'Ortega', '2018-08-12', '2018-10-12');


--
-- TOC entry 2884 (class 0 OID 16421)
-- Dependencies: 206
-- Data for Name: tipo_producto; Type: TABLE DATA; Schema: gym; Owner: postgres
--

INSERT INTO gym.tipo_producto (clave_tipo_producto, tipo_producto) VALUES (1, 'Producto');
INSERT INTO gym.tipo_producto (clave_tipo_producto, tipo_producto) VALUES (2, 'Servicio');


--
-- TOC entry 2886 (class 0 OID 16427)
-- Dependencies: 208
-- Data for Name: tipos_ventas; Type: TABLE DATA; Schema: gym; Owner: postgres
--

INSERT INTO gym.tipos_ventas (clave_tipo_venta, tipo_venta) VALUES (1, 'Suscripcion a servicio');
INSERT INTO gym.tipos_ventas (clave_tipo_venta, tipo_venta) VALUES (2, 'Pago de servicio');
INSERT INTO gym.tipos_ventas (clave_tipo_venta, tipo_venta) VALUES (3, 'Compra de producto');
INSERT INTO gym.tipos_ventas (clave_tipo_venta, tipo_venta) VALUES (4, 'Visita');


--
-- TOC entry 2891 (class 0 OID 16507)
-- Dependencies: 213
-- Data for Name: venta_producto; Type: TABLE DATA; Schema: gym; Owner: postgres
--

INSERT INTO gym.venta_producto (clave_producto, clave_venta, clave_tipo_venta, precio_producto, cantidad) VALUES (2, 4, 1, 690.00, 1);
INSERT INTO gym.venta_producto (clave_producto, clave_venta, clave_tipo_venta, precio_producto, cantidad) VALUES (2, 5, 2, 690.00, 1);
INSERT INTO gym.venta_producto (clave_producto, clave_venta, clave_tipo_venta, precio_producto, cantidad) VALUES (4, 6, 3, 2500.00, 5);
INSERT INTO gym.venta_producto (clave_producto, clave_venta, clave_tipo_venta, precio_producto, cantidad) VALUES (1, 6, 4, 100.00, 2);


--
-- TOC entry 2893 (class 0 OID 16513)
-- Dependencies: 215
-- Data for Name: ventas; Type: TABLE DATA; Schema: gym; Owner: postgres
--

INSERT INTO gym.ventas (clave_venta, clave_socio, fecha_venta) VALUES (4, 1, '2018-08-12');
INSERT INTO gym.ventas (clave_venta, clave_socio, fecha_venta) VALUES (5, 1, '2018-08-12');
INSERT INTO gym.ventas (clave_venta, clave_socio, fecha_venta) VALUES (5, 1, '2018-08-12');
INSERT INTO gym.ventas (clave_venta, clave_socio, fecha_venta) VALUES (6, NULL, '2018-08-12');


--
-- TOC entry 2888 (class 0 OID 16442)
-- Dependencies: 210
-- Data for Name: tipo_usuarios; Type: TABLE DATA; Schema: usrs; Owner: postgres
--

INSERT INTO usrs.tipo_usuarios (id_tipo_usuario, tipo_usuario, gestor_usuarios, gestor_socios, gestor_productos, gestor_empleados) VALUES (1, 'Administrador', NULL, NULL, NULL, NULL);
INSERT INTO usrs.tipo_usuarios (id_tipo_usuario, tipo_usuario, gestor_usuarios, gestor_socios, gestor_productos, gestor_empleados) VALUES (2, 'Empleado', NULL, NULL, NULL, NULL);


--
-- TOC entry 2890 (class 0 OID 16448)
-- Dependencies: 212
-- Data for Name: usuarios; Type: TABLE DATA; Schema: usrs; Owner: postgres
--



--
-- TOC entry 2911 (class 0 OID 0)
-- Dependencies: 198
-- Name: precios_clave_producto_seq; Type: SEQUENCE SET; Schema: gym; Owner: postgres
--

SELECT pg_catalog.setval('gym.precios_clave_producto_seq', 1, false);


--
-- TOC entry 2912 (class 0 OID 0)
-- Dependencies: 200
-- Name: productos_clave_producto_seq; Type: SEQUENCE SET; Schema: gym; Owner: postgres
--

SELECT pg_catalog.setval('gym.productos_clave_producto_seq', 1, false);


--
-- TOC entry 2913 (class 0 OID 0)
-- Dependencies: 203
-- Name: socios_clave_socio_seq; Type: SEQUENCE SET; Schema: gym; Owner: postgres
--

SELECT pg_catalog.setval('gym.socios_clave_socio_seq', 1, true);


--
-- TOC entry 2914 (class 0 OID 0)
-- Dependencies: 205
-- Name: tipo_producto_clave_tipo_producto_seq; Type: SEQUENCE SET; Schema: gym; Owner: postgres
--

SELECT pg_catalog.setval('gym.tipo_producto_clave_tipo_producto_seq', 1, false);


--
-- TOC entry 2915 (class 0 OID 0)
-- Dependencies: 207
-- Name: tipos_ventas_clave_tipo_venta_seq; Type: SEQUENCE SET; Schema: gym; Owner: postgres
--

SELECT pg_catalog.setval('gym.tipos_ventas_clave_tipo_venta_seq', 1, false);


--
-- TOC entry 2916 (class 0 OID 0)
-- Dependencies: 214
-- Name: ventas_clave_venta_seq; Type: SEQUENCE SET; Schema: gym; Owner: postgres
--

SELECT pg_catalog.setval('gym.ventas_clave_venta_seq', 5, true);


--
-- TOC entry 2917 (class 0 OID 0)
-- Dependencies: 209
-- Name: tipo_usuarios_id_tipo_usuario_seq; Type: SEQUENCE SET; Schema: usrs; Owner: postgres
--

SELECT pg_catalog.setval('usrs.tipo_usuarios_id_tipo_usuario_seq', 1, false);


--
-- TOC entry 2918 (class 0 OID 0)
-- Dependencies: 211
-- Name: usuarios_id_usuario_seq; Type: SEQUENCE SET; Schema: usrs; Owner: postgres
--

SELECT pg_catalog.setval('usrs.usuarios_id_usuario_seq', 1, false);


--
-- TOC entry 2736 (class 2606 OID 16453)
-- Name: precios precios_pkey; Type: CONSTRAINT; Schema: gym; Owner: postgres
--

ALTER TABLE ONLY gym.precios
    ADD CONSTRAINT precios_pkey PRIMARY KEY (clave_producto, fecha_alta);


--
-- TOC entry 2738 (class 2606 OID 16455)
-- Name: productos productos_pkey; Type: CONSTRAINT; Schema: gym; Owner: postgres
--

ALTER TABLE ONLY gym.productos
    ADD CONSTRAINT productos_pkey PRIMARY KEY (clave_producto);


--
-- TOC entry 2740 (class 2606 OID 16457)
-- Name: simetrias simetrias_pkey; Type: CONSTRAINT; Schema: gym; Owner: postgres
--

ALTER TABLE ONLY gym.simetrias
    ADD CONSTRAINT simetrias_pkey PRIMARY KEY (clave_socio);


--
-- TOC entry 2742 (class 2606 OID 16459)
-- Name: socios socios_pkey; Type: CONSTRAINT; Schema: gym; Owner: postgres
--

ALTER TABLE ONLY gym.socios
    ADD CONSTRAINT socios_pkey PRIMARY KEY (clave_socio);


--
-- TOC entry 2744 (class 2606 OID 16461)
-- Name: tipo_producto tipo_producto_pkey; Type: CONSTRAINT; Schema: gym; Owner: postgres
--

ALTER TABLE ONLY gym.tipo_producto
    ADD CONSTRAINT tipo_producto_pkey PRIMARY KEY (clave_tipo_producto);


--
-- TOC entry 2746 (class 2606 OID 16463)
-- Name: tipos_ventas tipos_ventas_pkey; Type: CONSTRAINT; Schema: gym; Owner: postgres
--

ALTER TABLE ONLY gym.tipos_ventas
    ADD CONSTRAINT tipos_ventas_pkey PRIMARY KEY (clave_tipo_venta);


--
-- TOC entry 2748 (class 2606 OID 16469)
-- Name: tipo_usuarios tipo_usuarios_pkey; Type: CONSTRAINT; Schema: usrs; Owner: postgres
--

ALTER TABLE ONLY usrs.tipo_usuarios
    ADD CONSTRAINT tipo_usuarios_pkey PRIMARY KEY (id_tipo_usuario);


--
-- TOC entry 2750 (class 2606 OID 16471)
-- Name: usuarios usuarios_pkey; Type: CONSTRAINT; Schema: usrs; Owner: postgres
--

ALTER TABLE ONLY usrs.usuarios
    ADD CONSTRAINT usuarios_pkey PRIMARY KEY (id_usuario);


--
-- TOC entry 2751 (class 2606 OID 16472)
-- Name: precios precios_clave_producto_fkey; Type: FK CONSTRAINT; Schema: gym; Owner: postgres
--

ALTER TABLE ONLY gym.precios
    ADD CONSTRAINT precios_clave_producto_fkey FOREIGN KEY (clave_producto) REFERENCES gym.productos(clave_producto) ON DELETE CASCADE;


--
-- TOC entry 2752 (class 2606 OID 16477)
-- Name: productos productos_clave_tipo_producto_fkey; Type: FK CONSTRAINT; Schema: gym; Owner: postgres
--

ALTER TABLE ONLY gym.productos
    ADD CONSTRAINT productos_clave_tipo_producto_fkey FOREIGN KEY (clave_tipo_producto) REFERENCES gym.tipo_producto(clave_tipo_producto) ON DELETE CASCADE;


--
-- TOC entry 2753 (class 2606 OID 16497)
-- Name: usuarios usuarios_id_tipo_usuario_fkey; Type: FK CONSTRAINT; Schema: usrs; Owner: postgres
--

ALTER TABLE ONLY usrs.usuarios
    ADD CONSTRAINT usuarios_id_tipo_usuario_fkey FOREIGN KEY (id_tipo_usuario) REFERENCES usrs.tipo_usuarios(id_tipo_usuario) ON DELETE CASCADE;


--
-- TOC entry 2901 (class 0 OID 0)
-- Dependencies: 6
-- Name: SCHEMA public; Type: ACL; Schema: -; Owner: postgres
--

GRANT ALL ON SCHEMA public TO PUBLIC;

create view gym.vista_venta_suscripcion_socio as select clave_producto,v.clave_venta,clave_tipo_venta,s.clave_socio,v.fecha_venta,s.fecha_inicio  from gym.venta_producto as vp join gym.ventas as v on vp.clave_venta = v.clave_venta join gym.socios as s on v.clave_socio = s.clave_socio;
create view gym.vista_info_productos as select prod.clave_producto,prod.clave_tipo_producto,prod.nombre_producto,prod.duracion,pre.fecha_alta,pre.fecha_expiracion,pre.precio,tip.tipo_producto from gym.productos as prod join gym.precios as pre on pre.clave_producto = prod.clave_producto join gym.tipo_producto as tip on prod.clave_tipo_producto = tip.clave_tipo_producto;
create view gym.vista_detalle_venta as select vp.clave_producto as Codigo,vp.precio_producto as Importe,vp.cantidad as Cantidad,tv.tipo_venta as Descripción,prod.nombre_producto as Producto,pre.precio as Costo from gym.venta_producto as vp join gym.tipos_ventas as tv on vp.clave_tipo_venta = tv.clave_tipo_venta join gym.productos as prod on vp.clave_producto = prod.clave_producto join gym.precios as pre on vp.clave_producto = pre.clave_producto


-- Completed on 2018-08-13 01:21:39

--
-- PostgreSQL database dump complete
--

