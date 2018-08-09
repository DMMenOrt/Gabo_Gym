--
-- PostgreSQL database dump
--

-- Dumped from database version 9.6.1
-- Dumped by pg_dump version 9.6.1

-- Started on 2018-08-05 20:50:47

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 6 (class 2615 OID 25191)
-- Name: gym; Type: SCHEMA; Schema: -; Owner: gabo_adm
--
CREATE USER gabo_adm WITH SUPERUSER CREATEDB CREATEROLE INHERIT LOGIN REPLICATION BYPASSRLS 
ENCRYPTED PASSWORD 'g@bo_gym';

CREATE USER gabo_emp WITH NOSUPERUSER LOGIN NOCREATEDB NOCREATEROLE INHERIT REPLICATION ENCRYPTED 
PASSWORD 'g@bo_emp';
CREATE SCHEMA gym;


ALTER SCHEMA gym OWNER TO gabo_adm;

--
-- TOC entry 4 (class 2615 OID 25192)
-- Name: usrs; Type: SCHEMA; Schema: -; Owner: gabo_adm
--

CREATE SCHEMA usrs;


ALTER SCHEMA usrs OWNER TO gabo_adm;

SET search_path = gym, pg_catalog;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 201 (class 1259 OID 25355)
-- Name: precios; Type: TABLE; Schema: gym; Owner: gabo_adm
--

CREATE TABLE precios (
    clave_producto integer NOT NULL,
    fecha_alta date NOT NULL,
    precio numeric(5,2),
    fecha_expiracion date
);


ALTER TABLE precios OWNER TO gabo_adm;

--
-- TOC entry 200 (class 1259 OID 25344)
-- Name: productos; Type: TABLE; Schema: gym; Owner: gabo_adm
--

CREATE TABLE productos (
    clave_producto integer NOT NULL,
    clave_tipo_producto integer,
    nombre_producto character varying(40),
    duracion integer
);


ALTER TABLE productos OWNER TO gabo_adm;

--
-- TOC entry 199 (class 1259 OID 25342)
-- Name: productos_clave_producto_seq; Type: SEQUENCE; Schema: gym; Owner: gabo_adm
--

CREATE SEQUENCE productos_clave_producto_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE productos_clave_producto_seq OWNER TO gabo_adm;

--
-- TOC entry 2227 (class 0 OID 0)
-- Dependencies: 199
-- Name: productos_clave_producto_seq; Type: SEQUENCE OWNED BY; Schema: gym; Owner: gabo_adm
--

ALTER SEQUENCE productos_clave_producto_seq OWNED BY productos.clave_producto;


--
-- TOC entry 194 (class 1259 OID 25245)
-- Name: simetrias; Type: TABLE; Schema: gym; Owner: gabo_adm
--

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


ALTER TABLE simetrias OWNER TO gabo_adm;

--
-- TOC entry 193 (class 1259 OID 25243)
-- Name: simetrias_clave_socio_seq; Type: SEQUENCE; Schema: gym; Owner: gabo_adm
--

CREATE SEQUENCE simetrias_clave_socio_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE simetrias_clave_socio_seq OWNER TO gabo_adm;

--
-- TOC entry 2228 (class 0 OID 0)
-- Dependencies: 193
-- Name: simetrias_clave_socio_seq; Type: SEQUENCE OWNED BY; Schema: gym; Owner: gabo_adm
--

ALTER SEQUENCE simetrias_clave_socio_seq OWNED BY simetrias.clave_socio;


--
-- TOC entry 192 (class 1259 OID 25237)
-- Name: socios; Type: TABLE; Schema: gym; Owner: gabo_adm
--

CREATE TABLE socios (
    clave_socio integer NOT NULL,
    nombre character varying(40),
    primer_apellido character varying(20),
    segundo_apellido character varying(20),
    fecha_inicio date,
    fecha_fin date
);


ALTER TABLE socios OWNER TO gabo_adm;

--
-- TOC entry 191 (class 1259 OID 25235)
-- Name: socios_clave_socio_seq; Type: SEQUENCE; Schema: gym; Owner: gabo_adm
--

CREATE SEQUENCE socios_clave_socio_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE socios_clave_socio_seq OWNER TO gabo_adm;

--
-- TOC entry 2229 (class 0 OID 0)
-- Dependencies: 191
-- Name: socios_clave_socio_seq; Type: SEQUENCE OWNED BY; Schema: gym; Owner: gabo_adm
--

ALTER SEQUENCE socios_clave_socio_seq OWNED BY socios.clave_socio;


--
-- TOC entry 188 (class 1259 OID 25195)
-- Name: tipo_producto; Type: TABLE; Schema: gym; Owner: gabo_adm
--

CREATE TABLE tipo_producto (
    clave_tipo_producto integer NOT NULL,
    tipo_producto character varying(20)
);


ALTER TABLE tipo_producto OWNER TO gabo_adm;

--
-- TOC entry 187 (class 1259 OID 25193)
-- Name: tipo_producto_clave_tipo_producto_seq; Type: SEQUENCE; Schema: gym; Owner: gabo_adm
--

CREATE SEQUENCE tipo_producto_clave_tipo_producto_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE tipo_producto_clave_tipo_producto_seq OWNER TO gabo_adm;

--
-- TOC entry 2230 (class 0 OID 0)
-- Dependencies: 187
-- Name: tipo_producto_clave_tipo_producto_seq; Type: SEQUENCE OWNED BY; Schema: gym; Owner: gabo_adm
--

ALTER SEQUENCE tipo_producto_clave_tipo_producto_seq OWNED BY tipo_producto.clave_tipo_producto;


--
-- TOC entry 190 (class 1259 OID 25229)
-- Name: tipos_ventas; Type: TABLE; Schema: gym; Owner: gabo_adm
--

CREATE TABLE tipos_ventas (
    clave_tipo_venta integer NOT NULL,
    tipo_venta character varying(30)
);


ALTER TABLE tipos_ventas OWNER TO gabo_adm;

--
-- TOC entry 189 (class 1259 OID 25227)
-- Name: tipos_ventas_clave_tipo_venta_seq; Type: SEQUENCE; Schema: gym; Owner: gabo_adm
--

CREATE SEQUENCE tipos_ventas_clave_tipo_venta_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE tipos_ventas_clave_tipo_venta_seq OWNER TO gabo_adm;

--
-- TOC entry 2231 (class 0 OID 0)
-- Dependencies: 189
-- Name: tipos_ventas_clave_tipo_venta_seq; Type: SEQUENCE OWNED BY; Schema: gym; Owner: gabo_adm
--

ALTER SEQUENCE tipos_ventas_clave_tipo_venta_seq OWNED BY tipos_ventas.clave_tipo_venta;


--
-- TOC entry 204 (class 1259 OID 25403)
-- Name: venta_producto; Type: TABLE; Schema: gym; Owner: gabo_adm
--

CREATE TABLE venta_producto (
    clave_producto integer NOT NULL,
    clave_venta integer NOT NULL,
    precio_producto numeric(5,2)
);


ALTER TABLE venta_producto OWNER TO gabo_adm;

--
-- TOC entry 203 (class 1259 OID 25377)
-- Name: ventas; Type: TABLE; Schema: gym; Owner: gabo_adm
--

CREATE TABLE ventas (
    clave_venta integer NOT NULL,
    clave_tipo_venta integer,
    clave_socio integer,
    fecha_venta date
);


ALTER TABLE ventas OWNER TO gabo_adm;

--
-- TOC entry 202 (class 1259 OID 25375)
-- Name: ventas_clave_venta_seq; Type: SEQUENCE; Schema: gym; Owner: gabo_adm
--

CREATE SEQUENCE ventas_clave_venta_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE ventas_clave_venta_seq OWNER TO gabo_adm;

--
-- TOC entry 2232 (class 0 OID 0)
-- Dependencies: 202
-- Name: ventas_clave_venta_seq; Type: SEQUENCE OWNED BY; Schema: gym; Owner: gabo_adm
--

ALTER SEQUENCE ventas_clave_venta_seq OWNED BY ventas.clave_venta;


SET search_path = usrs, pg_catalog;

--
-- TOC entry 196 (class 1259 OID 25261)
-- Name: tipo_usuarios; Type: TABLE; Schema: usrs; Owner: gabo_adm
--

CREATE TABLE tipo_usuarios (
    id_tipo_usuario integer NOT NULL,
    tipo_usuario character varying(40),
    gestor_usuarios boolean,
    gestor_socios boolean,
    gestor_productos boolean,
    gestor_empleados boolean
);


ALTER TABLE tipo_usuarios OWNER TO gabo_adm;

--
-- TOC entry 195 (class 1259 OID 25259)
-- Name: tipo_usuarios_id_tipo_usuario_seq; Type: SEQUENCE; Schema: usrs; Owner: gabo_adm
--

CREATE SEQUENCE tipo_usuarios_id_tipo_usuario_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE tipo_usuarios_id_tipo_usuario_seq OWNER TO gabo_adm;

--
-- TOC entry 2233 (class 0 OID 0)
-- Dependencies: 195
-- Name: tipo_usuarios_id_tipo_usuario_seq; Type: SEQUENCE OWNED BY; Schema: usrs; Owner: gabo_adm
--

ALTER SEQUENCE tipo_usuarios_id_tipo_usuario_seq OWNED BY tipo_usuarios.id_tipo_usuario;


--
-- TOC entry 198 (class 1259 OID 25321)
-- Name: usuarios; Type: TABLE; Schema: usrs; Owner: gabo_adm
--

CREATE TABLE usuarios (
    id_usuario integer NOT NULL,
    nombre_usuario character varying(40),
    nombre character varying(40),
    primer_apellido character varying(40),
    segundo_apellido character varying(40),
    id_tipo_usuario integer
);


ALTER TABLE usuarios OWNER TO gabo_adm;

--
-- TOC entry 197 (class 1259 OID 25319)
-- Name: usuarios_id_usuario_seq; Type: SEQUENCE; Schema: usrs; Owner: gabo_adm
--

CREATE SEQUENCE usuarios_id_usuario_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE usuarios_id_usuario_seq OWNER TO gabo_adm;

--
-- TOC entry 2234 (class 0 OID 0)
-- Dependencies: 197
-- Name: usuarios_id_usuario_seq; Type: SEQUENCE OWNED BY; Schema: usrs; Owner: gabo_adm
--

ALTER SEQUENCE usuarios_id_usuario_seq OWNED BY usuarios.id_usuario;


SET search_path = gym, pg_catalog;

--
-- TOC entry 2059 (class 2604 OID 25347)
-- Name: productos clave_producto; Type: DEFAULT; Schema: gym; Owner: gabo_adm
--

ALTER TABLE ONLY productos ALTER COLUMN clave_producto SET DEFAULT nextval('productos_clave_producto_seq'::regclass);


--
-- TOC entry 2056 (class 2604 OID 25248)
-- Name: simetrias clave_socio; Type: DEFAULT; Schema: gym; Owner: gabo_adm
--

ALTER TABLE ONLY simetrias ALTER COLUMN clave_socio SET DEFAULT nextval('simetrias_clave_socio_seq'::regclass);


--
-- TOC entry 2055 (class 2604 OID 25240)
-- Name: socios clave_socio; Type: DEFAULT; Schema: gym; Owner: gabo_adm
--

ALTER TABLE ONLY socios ALTER COLUMN clave_socio SET DEFAULT nextval('socios_clave_socio_seq'::regclass);


--
-- TOC entry 2053 (class 2604 OID 25198)
-- Name: tipo_producto clave_tipo_producto; Type: DEFAULT; Schema: gym; Owner: gabo_adm
--

ALTER TABLE ONLY tipo_producto ALTER COLUMN clave_tipo_producto SET DEFAULT nextval('tipo_producto_clave_tipo_producto_seq'::regclass);


--
-- TOC entry 2054 (class 2604 OID 25232)
-- Name: tipos_ventas clave_tipo_venta; Type: DEFAULT; Schema: gym; Owner: gabo_adm
--

ALTER TABLE ONLY tipos_ventas ALTER COLUMN clave_tipo_venta SET DEFAULT nextval('tipos_ventas_clave_tipo_venta_seq'::regclass);


--
-- TOC entry 2060 (class 2604 OID 25380)
-- Name: ventas clave_venta; Type: DEFAULT; Schema: gym; Owner: gabo_adm
--

ALTER TABLE ONLY ventas ALTER COLUMN clave_venta SET DEFAULT nextval('ventas_clave_venta_seq'::regclass);


SET search_path = usrs, pg_catalog;

--
-- TOC entry 2057 (class 2604 OID 25264)
-- Name: tipo_usuarios id_tipo_usuario; Type: DEFAULT; Schema: usrs; Owner: gabo_adm
--

ALTER TABLE ONLY tipo_usuarios ALTER COLUMN id_tipo_usuario SET DEFAULT nextval('tipo_usuarios_id_tipo_usuario_seq'::regclass);


--
-- TOC entry 2058 (class 2604 OID 25324)
-- Name: usuarios id_usuario; Type: DEFAULT; Schema: usrs; Owner: gabo_adm
--

ALTER TABLE ONLY usuarios ALTER COLUMN id_usuario SET DEFAULT nextval('usuarios_id_usuario_seq'::regclass);


SET search_path = gym, pg_catalog;

--
-- TOC entry 2219 (class 0 OID 25355)
-- Dependencies: 201
-- Data for Name: precios; Type: TABLE DATA; Schema: gym; Owner: gabo_adm
--



--
-- TOC entry 2218 (class 0 OID 25344)
-- Dependencies: 200
-- Data for Name: productos; Type: TABLE DATA; Schema: gym; Owner: gabo_adm
--



--
-- TOC entry 2235 (class 0 OID 0)
-- Dependencies: 199
-- Name: productos_clave_producto_seq; Type: SEQUENCE SET; Schema: gym; Owner: gabo_adm
--

SELECT pg_catalog.setval('productos_clave_producto_seq', 1, false);


--
-- TOC entry 2212 (class 0 OID 25245)
-- Dependencies: 194
-- Data for Name: simetrias; Type: TABLE DATA; Schema: gym; Owner: gabo_adm
--



--
-- TOC entry 2236 (class 0 OID 0)
-- Dependencies: 193
-- Name: simetrias_clave_socio_seq; Type: SEQUENCE SET; Schema: gym; Owner: gabo_adm
--

SELECT pg_catalog.setval('simetrias_clave_socio_seq', 1, false);


--
-- TOC entry 2210 (class 0 OID 25237)
-- Dependencies: 192
-- Data for Name: socios; Type: TABLE DATA; Schema: gym; Owner: gabo_adm
--



--
-- TOC entry 2237 (class 0 OID 0)
-- Dependencies: 191
-- Name: socios_clave_socio_seq; Type: SEQUENCE SET; Schema: gym; Owner: gabo_adm
--

SELECT pg_catalog.setval('socios_clave_socio_seq', 1, false);


--
-- TOC entry 2206 (class 0 OID 25195)
-- Dependencies: 188
-- Data for Name: tipo_producto; Type: TABLE DATA; Schema: gym; Owner: gabo_adm
--



--
-- TOC entry 2238 (class 0 OID 0)
-- Dependencies: 187
-- Name: tipo_producto_clave_tipo_producto_seq; Type: SEQUENCE SET; Schema: gym; Owner: gabo_adm
--

SELECT pg_catalog.setval('tipo_producto_clave_tipo_producto_seq', 1, false);


--
-- TOC entry 2208 (class 0 OID 25229)
-- Dependencies: 190
-- Data for Name: tipos_ventas; Type: TABLE DATA; Schema: gym; Owner: gabo_adm
--



--
-- TOC entry 2239 (class 0 OID 0)
-- Dependencies: 189
-- Name: tipos_ventas_clave_tipo_venta_seq; Type: SEQUENCE SET; Schema: gym; Owner: gabo_adm
--

SELECT pg_catalog.setval('tipos_ventas_clave_tipo_venta_seq', 1, false);


--
-- TOC entry 2222 (class 0 OID 25403)
-- Dependencies: 204
-- Data for Name: venta_producto; Type: TABLE DATA; Schema: gym; Owner: gabo_adm
--



--
-- TOC entry 2221 (class 0 OID 25377)
-- Dependencies: 203
-- Data for Name: ventas; Type: TABLE DATA; Schema: gym; Owner: gabo_adm
--



--
-- TOC entry 2240 (class 0 OID 0)
-- Dependencies: 202
-- Name: ventas_clave_venta_seq; Type: SEQUENCE SET; Schema: gym; Owner: gabo_adm
--

SELECT pg_catalog.setval('ventas_clave_venta_seq', 1, false);


SET search_path = usrs, pg_catalog;

--
-- TOC entry 2214 (class 0 OID 25261)
-- Dependencies: 196
-- Data for Name: tipo_usuarios; Type: TABLE DATA; Schema: usrs; Owner: gabo_adm
--



--
-- TOC entry 2241 (class 0 OID 0)
-- Dependencies: 195
-- Name: tipo_usuarios_id_tipo_usuario_seq; Type: SEQUENCE SET; Schema: usrs; Owner: gabo_adm
--

SELECT pg_catalog.setval('tipo_usuarios_id_tipo_usuario_seq', 1, false);


--
-- TOC entry 2216 (class 0 OID 25321)
-- Dependencies: 198
-- Data for Name: usuarios; Type: TABLE DATA; Schema: usrs; Owner: gabo_adm
--



--
-- TOC entry 2242 (class 0 OID 0)
-- Dependencies: 197
-- Name: usuarios_id_usuario_seq; Type: SEQUENCE SET; Schema: usrs; Owner: gabo_adm
--

SELECT pg_catalog.setval('usuarios_id_usuario_seq', 1, false);


SET search_path = gym, pg_catalog;

--
-- TOC entry 2076 (class 2606 OID 25359)
-- Name: precios precios_pkey; Type: CONSTRAINT; Schema: gym; Owner: gabo_adm
--

ALTER TABLE ONLY precios
    ADD CONSTRAINT precios_pkey PRIMARY KEY (clave_producto, fecha_alta);


--
-- TOC entry 2074 (class 2606 OID 25349)
-- Name: productos productos_pkey; Type: CONSTRAINT; Schema: gym; Owner: gabo_adm
--

ALTER TABLE ONLY productos
    ADD CONSTRAINT productos_pkey PRIMARY KEY (clave_producto);


--
-- TOC entry 2068 (class 2606 OID 25250)
-- Name: simetrias simetrias_pkey; Type: CONSTRAINT; Schema: gym; Owner: gabo_adm
--

ALTER TABLE ONLY simetrias
    ADD CONSTRAINT simetrias_pkey PRIMARY KEY (clave_socio);


--
-- TOC entry 2066 (class 2606 OID 25242)
-- Name: socios socios_pkey; Type: CONSTRAINT; Schema: gym; Owner: gabo_adm
--

ALTER TABLE ONLY socios
    ADD CONSTRAINT socios_pkey PRIMARY KEY (clave_socio);


--
-- TOC entry 2062 (class 2606 OID 25200)
-- Name: tipo_producto tipo_producto_pkey; Type: CONSTRAINT; Schema: gym; Owner: gabo_adm
--

ALTER TABLE ONLY tipo_producto
    ADD CONSTRAINT tipo_producto_pkey PRIMARY KEY (clave_tipo_producto);


--
-- TOC entry 2064 (class 2606 OID 25234)
-- Name: tipos_ventas tipos_ventas_pkey; Type: CONSTRAINT; Schema: gym; Owner: gabo_adm
--

ALTER TABLE ONLY tipos_ventas
    ADD CONSTRAINT tipos_ventas_pkey PRIMARY KEY (clave_tipo_venta);


--
-- TOC entry 2080 (class 2606 OID 25407)
-- Name: venta_producto venta_producto_pkey; Type: CONSTRAINT; Schema: gym; Owner: gabo_adm
--

ALTER TABLE ONLY venta_producto
    ADD CONSTRAINT venta_producto_pkey PRIMARY KEY (clave_producto, clave_venta);


--
-- TOC entry 2078 (class 2606 OID 25382)
-- Name: ventas ventas_pkey; Type: CONSTRAINT; Schema: gym; Owner: gabo_adm
--

ALTER TABLE ONLY ventas
    ADD CONSTRAINT ventas_pkey PRIMARY KEY (clave_venta);


SET search_path = usrs, pg_catalog;

--
-- TOC entry 2070 (class 2606 OID 25266)
-- Name: tipo_usuarios tipo_usuarios_pkey; Type: CONSTRAINT; Schema: usrs; Owner: gabo_adm
--

ALTER TABLE ONLY tipo_usuarios
    ADD CONSTRAINT tipo_usuarios_pkey PRIMARY KEY (id_tipo_usuario);


--
-- TOC entry 2072 (class 2606 OID 25326)
-- Name: usuarios usuarios_pkey; Type: CONSTRAINT; Schema: usrs; Owner: gabo_adm
--

ALTER TABLE ONLY usuarios
    ADD CONSTRAINT usuarios_pkey PRIMARY KEY (id_usuario);


SET search_path = gym, pg_catalog;

--
-- TOC entry 2083 (class 2606 OID 25360)
-- Name: precios precios_clave_producto_fkey; Type: FK CONSTRAINT; Schema: gym; Owner: gabo_adm
--

ALTER TABLE ONLY precios
    ADD CONSTRAINT precios_clave_producto_fkey FOREIGN KEY (clave_producto) REFERENCES productos(clave_producto);


--
-- TOC entry 2082 (class 2606 OID 25350)
-- Name: productos productos_clave_tipo_producto_fkey; Type: FK CONSTRAINT; Schema: gym; Owner: gabo_adm
--

ALTER TABLE ONLY productos
    ADD CONSTRAINT productos_clave_tipo_producto_fkey FOREIGN KEY (clave_tipo_producto) REFERENCES tipo_producto(clave_tipo_producto);


--
-- TOC entry 2086 (class 2606 OID 25408)
-- Name: venta_producto venta_producto_clave_producto_fkey; Type: FK CONSTRAINT; Schema: gym; Owner: gabo_adm
--

ALTER TABLE ONLY venta_producto
    ADD CONSTRAINT venta_producto_clave_producto_fkey FOREIGN KEY (clave_producto) REFERENCES productos(clave_producto);


--
-- TOC entry 2087 (class 2606 OID 25413)
-- Name: venta_producto venta_producto_clave_venta_fkey; Type: FK CONSTRAINT; Schema: gym; Owner: gabo_adm
--

ALTER TABLE ONLY venta_producto
    ADD CONSTRAINT venta_producto_clave_venta_fkey FOREIGN KEY (clave_venta) REFERENCES ventas(clave_venta);


--
-- TOC entry 2085 (class 2606 OID 25388)
-- Name: ventas ventas_clave_socio_fkey; Type: FK CONSTRAINT; Schema: gym; Owner: gabo_adm
--

ALTER TABLE ONLY ventas
    ADD CONSTRAINT ventas_clave_socio_fkey FOREIGN KEY (clave_socio) REFERENCES socios(clave_socio);


--
-- TOC entry 2084 (class 2606 OID 25383)
-- Name: ventas ventas_clave_tipo_venta_fkey; Type: FK CONSTRAINT; Schema: gym; Owner: gabo_adm
--

ALTER TABLE ONLY ventas
    ADD CONSTRAINT ventas_clave_tipo_venta_fkey FOREIGN KEY (clave_tipo_venta) REFERENCES tipos_ventas(clave_tipo_venta);


SET search_path = usrs, pg_catalog;

--
-- TOC entry 2081 (class 2606 OID 25327)
-- Name: usuarios usuarios_id_tipo_usuario_fkey; Type: FK CONSTRAINT; Schema: usrs; Owner: gabo_adm
--

ALTER TABLE ONLY usuarios
    ADD CONSTRAINT usuarios_id_tipo_usuario_fkey FOREIGN KEY (id_tipo_usuario) REFERENCES tipo_usuarios(id_tipo_usuario);


-- Completed on 2018-08-05 20:50:48

--
-- PostgreSQL database dump complete
--

