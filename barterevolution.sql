-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 05-12-2020 a las 01:40:24
-- Versión del servidor: 10.4.11-MariaDB
-- Versión de PHP: 7.4.6

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `barterevolution`
--

DELIMITER $$
--
-- Procedimientos
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `clientes_varios_contratos` ()  SELECT c.Cedula_cliente, cl.Nombrecliente1, cl.Apellidocliente1, c.No_contrato, c.No_prorroga, ccon.Nombre_condicioncon, art.Nombre_articulo
FROM contratos c JOIN clientes cl ON c.Cedula_cliente=cl.Cedula_cliente
JOIN condicion_contratos ccon ON c.Id_condicion_contrato=ccon.Id_condicion_contrato
JOIN articulos art ON c.Id_articulo=art.IdArticulo
    WHERE c.Cedula_cliente IN (
        SELECT Cedula_cliente FROM contratos 
            GROUP BY Cedula_cliente 
            HAVING COUNT(*)>1        
        )
        ORDER by cl.Nombrecliente1$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `contratos_con_sus_prorrogas` ()  SELECT cont.No_contrato, concat(cli.nombrecliente1,' ',cli.apellidocliente1) as Clientes, cli.Id_tipocliente, tipc.Nombre_tipocliente, cont.Valor_contrato, pro.No_prorroga, pro.Valor_mes as Valor_Prorroga, cont.Fecha_inicio, cont.Fecha_vencimiento, pro.Fecha_inicio_prorroga, pro.Fecha_vencimiento_prorroga
FROM clientes AS cli INNER JOIN contratos AS cont ON cont.Cedula_cliente=cli.Cedula_cliente
INNER JOIN prorrogas pro ON cont.No_prorroga=pro.No_prorroga
INNER JOIN tipo_cliente tipc ON cli.Id_tipocliente=tipc.Id_tipocliente 
WHERE pro.No_prorroga >="PR-00001"
ORDER BY cont.No_contrato$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `validarcontrato` ()  SELECT No_contrato, DATEDIFF(CURDATE(), Fecha_inicio) diasContratos, CASE WHEN DATEDIFF(CURDATE(), Fecha_inicio) BETWEEN 10 AND 20 THEN 'Mas de diez días' WHEN DATEDIFF(CURDATE(), Fecha_inicio) BETWEEN 21 AND 30 THEN 'Mas de veinte días' WHEN DATEDIFF(CURDATE(), Fecha_inicio) >= 30 THEN 'Mas de treinta días' ELSE 'Menos de diez días' END RANGO FROM contratos WHERE contratos.Id_condicion_contrato="V"$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `articulos`
--

CREATE TABLE `articulos` (
  `IdArticulo` varchar(10) NOT NULL,
  `Nombre_articulo` varchar(25) NOT NULL,
  `Serie` varchar(20) DEFAULT NULL,
  `Modelo` varchar(15) DEFAULT NULL,
  `Marca` varchar(15) NOT NULL,
  `Precio_unitario` int(11) NOT NULL,
  `Descripcion` varchar(100) NOT NULL,
  `Genero` varchar(2) DEFAULT NULL,
  `Id_categoria` varchar(4) NOT NULL,
  `Id_estado_articulo` varchar(2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `articulos`
--

INSERT INTO `articulos` (`IdArticulo`, `Nombre_articulo`, `Serie`, `Modelo`, `Marca`, `Precio_unitario`, `Descripcion`, `Genero`, `Id_categoria`, `Id_estado_articulo`) VALUES
('0', '0', '0', '0', '0', 0, '0', NULL, 'JOY', 'M'),
('CYV00001', ' Consola de videojuego', ' YURE234143456', ' CLASSIC 620', ' MAYLUCK', 166000, ' Consola retro con dos controles', NULL, 'CYV', 'B'),
('CYV00002', ' Consola de videojuego', ' 566GHYT10000', ' MINI 4566', ' PLAYSTATION', 510000, ' Consola PS4 Slim 1tera', NULL, 'CYV', 'B'),
('CYV00003', ' Consola de videojuego', '19930495932', ' SLIM 8892', ' PLAYSTATION', 130000, ' Consola play mini classic', NULL, 'CYV', 'B'),
('CYV00004', ' Consola de videojuego', ' 84950SQW', ' GBA1234', ' NINTENDO', 80000, ' Consola mini retro un control ', NULL, 'CYV', 'B'),
('ELE00001', ' Nevera', '18000000123', ' PF00091', ' LG', 420000, ' Tamano grande de color negro', NULL, 'ELE', 'B'),
('ELE00002', ' Microondas', '1605665', ' ST0056', ' LG', 140000, ' Color gris Numeros borrosos tamano mediano', NULL, 'ELE', 'R'),
('ELE00003', ' Microondas', '18000023421', ' FGT567S', ' INCASA', 100000, ' Color blanco tamano mediano', NULL, 'ELE', 'B'),
('ELE00004', ' Nevera', ' 23456543FGH', ' QGHT12', ' HABBA', 64000, ' Tipo bar de color blanco', NULL, 'ELE', 'R'),
('ELE00005', ' Nevera', ' 456543W', ' KL 342', ' SAMSUNG', 220000, ' Color negro tamano grande', NULL, 'ELE', 'B'),
('ELE00006', ' Equipo de sonido', '2949503020', ' RTY45534', ' SONY', 90000, ' Color gris con bafles tamano mediano', NULL, 'ELE', 'B'),
('ELE00007', 'Sanduchera', '567QFGH', '0102', 'Abby', 50000, 'Color blanco de 2 puestos', NULL, 'ELE', 'B'),
('JOY00001', ' Anillo', '445', ' LWE 23', ' NA', 90000, ' Tipo mujer oro 18 kilates con zircon - talla 7', 'M', 'JOY', 'B'),
('JOY00002', ' Anillo', '3341', ' LEY 980', ' NA', 60000, ' Tipo hombre plata 5gr - talla 9', 'H', 'JOY', 'B'),
('JOY00003', ' Cadena', '3949', ' SLK32', ' NA', 280000, ' De oro 30 kilates con dije', 'H', 'JOY', 'B'),
('JOY00004', ' Cadena', ' 99SS', ' YUI2234', ' NA', 255000, ' De oro  24 kilates con doble dije', 'H', 'JOY', 'B'),
('JOY00005', ' Pulsera', ' 96989 QJ', ' JSIW 1-22', ' NA', 200000, ' De oro 18kilates con dije', 'M', 'JOY', 'B'),
('JOY00006', 'Anillo', '', '', 'NA', 95000, 'incrustacion de esmeralda talla 10 color plata', 'H', 'JOY', 'B'),
('REL00001', ' Reloj', ' PHH2301', ' QWR2010', ' CASIO', 115000, ' Con manilla de color plateado', 'H', 'REL', 'B'),
('REL00002', ' Reloj', ' WRT2334Q0', ' PKJ2332', ' CASIO', 160000, ' Con manilla de cuero', 'H', 'REL', 'B'),
('REL00003', ' Reloj', ' WSF2645', ' PQW2011', ' CASIO', 390000, ' Con manilla de color negro', 'M', 'REL', 'B'),
('REL00004', 'Reloj', 'WTYU65432', '56544', 'FOSSIL', 120000, 'Color plateado manual', 'H', 'REL', 'B'),
('TYO00001', ' Portatil', ' VBD567SO', ' G 460', ' LENOVO', 250000, ' Color blanco Teclado medio borroso de 17pg', NULL, 'TYO', 'B'),
('TYO00002', ' PC', ' FGHJ100043', ' M 871-1', ' HP', 380000, ' Color negro sin mouse', NULL, 'TYO', 'B'),
('TYO00003', ' Portatil', ' VBG456QP', ' A 112', ' AXUS', 90000, ' Pantalla color rojo de 15pg', NULL, 'TYO', 'R'),
('TYO00004', ' Tablet', ' POI112WRT', ' G 334', ' TOSHIBA', 55000, ' Pantalla rallada de 10pg', NULL, 'TYO', 'R'),
('TYO00005', ' Portatil', ' QOP097TP', ' L 557', ' DELL', 100000, ' Color negro de 20pg', NULL, 'TYO', 'B'),
('TYO00006', ' PC', ' SSW127PI', ' M 566-5', ' HP', 450000, ' Color negro No funciona camara', NULL, 'TYO', 'R'),
('TYO00007', ' Tablet', ' UQP980P1', ' G 990', ' LG', 70000, ' Color blanco de 11pg', NULL, 'TYO', 'B'),
('TYO00008', ' Televisor', ' 456DFGT1', ' QW 9001', ' KALLEY', 100000, ' TV plasma 20pg gris con control', NULL, 'TYO', 'R'),
('TYO00009', ' DVD', ' 55653GJ', ' 3455QE', ' SONY', 80000, ' Color gris tamano mediano con control', NULL, 'TYO', 'B'),
('TYO00010', ' DVD', ' 59303FF', ' MQW 34Q', ' LG', 59000, ' Color negro tamano pequeno sin control', NULL, 'TYO', 'B'),
('TYO00011', ' Televisor', '56775001', ' QWE120P', ' LG', 98000, ' TV plasma 30pg negro con control', NULL, 'TYO', 'R'),
('TYO00012', ' Televisor', ' FKGUI456', ' DJRI - 5990', ' LG', 95000, ' TV plasma 22pg negro sin control', NULL, 'TYO', 'B'),
('TYO00013', 'Portatil', '', 'G460', 'Lenovo', 480000, 'portatil negro de 18pg ', NULL, 'TYO', 'B');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `categorias`
--

CREATE TABLE `categorias` (
  `Id_categoria` varchar(4) NOT NULL,
  `Nombre_categoria` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `categorias`
--

INSERT INTO `categorias` (`Id_categoria`, `Nombre_categoria`) VALUES
('CAM', 'Cameras'),
('CYV', 'Video games and consoles'),
('ELE', 'Appliances'),
('HER', 'Tools'),
('JOY', 'Jewelry'),
('REL', 'Watches'),
('SI', 'Sport Items'),
('TYO', 'TV and Computers');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `clientes`
--

CREATE TABLE `clientes` (
  `Cedula_cliente` int(11) NOT NULL,
  `Id_documento` varchar(2) NOT NULL,
  `Id_tipocliente` varchar(2) NOT NULL,
  `Nombrecliente1` varchar(15) NOT NULL,
  `Nombrecliente2` varchar(15) DEFAULT NULL,
  `Apellidocliente1` varchar(15) NOT NULL,
  `Apellidocliente2` varchar(15) DEFAULT NULL,
  `Genero` varchar(2) NOT NULL,
  `Telefono_movil` bigint(10) DEFAULT NULL,
  `Email` varchar(35) DEFAULT NULL,
  `Direccion_residencia` varchar(25) NOT NULL,
  `Ciudad` varchar(10) NOT NULL,
  `Id_localidad` int(2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `clientes`
--

INSERT INTO `clientes` (`Cedula_cliente`, `Id_documento`, `Id_tipocliente`, `Nombrecliente1`, `Nombrecliente2`, `Apellidocliente1`, `Apellidocliente2`, `Genero`, `Telefono_movil`, `Email`, `Direccion_residencia`, `Ciudad`, `Id_localidad`) VALUES
(0, 'CE', 'C', 'N', 'N', 'N', 'N', 'M', 0, 'N', 'N', 'N', 1),
(2288911, 'CC', 'C', ' Amparo', NULL, 'Forero', 'Orjuela', 'M', 3195567345, 'amparoorjuela@email.com', 'Cll182No.113-144', 'Bogota DC', 14),
(2756722, 'CC', 'C', ' Misaelina', NULL, 'Ortega', NULL, 'M', 3141114567, NULL, 'Dg69No.58-148', 'Bogota DC', 14),
(10903456, 'CC', 'C', 'Eva', '', 'Mora', '', 'M', 3112237765, 'evamora@email.com', 'kra 12 k # 27 48 sur', 'Bogota DC', 11),
(35112376, 'CC', 'C', ' Juan', 'Camilo', 'Maldonado', 'Sanchez', 'H', 3004319321, 'jkmaldonados@outlook.com', 'Av19No.182-34', 'Bogota DC', 15),
(37985123, 'CC', 'C', ' Fausto ', 'Ferney', 'Franco', NULL, 'H', 3100013456, 'fffranco2021@hotmail.com', 'Av54No.79-21', 'Bogota DC', 5),
(41556345, 'CC', 'C', ' Sandra', NULL, 'Prieto', 'Gomez', 'M', 3058196480, 'sandraprietog201@yahoo.com', 'Tv139No167-24', 'Bogota DC', 19),
(45123477, 'CC', 'C', ' Cecilia', '', 'Herrera', 'Herrera', 'M', 3109228288, 'ccherrera12@yahoo.com', 'Cll106No.53-66', 'Bogota DC', 3),
(45223554, 'CC', 'C', ' Katherine', NULL, 'Serquea', 'Gomez', 'M', 3163101696, 'katerineserquera2000@outlook.es', 'Diagonal14No.138-162', 'Bogota DC', 5),
(47123873, 'CC', 'C', ' Miguel', 'Antonio', 'Ruiz', 'Cruz', 'H', 3003670272, 'miguelantoniorcruz@outlook.es', 'Kra158No.25-74', 'Bogota DC', 12),
(51889736, 'CC', 'C', ' Maria', 'Sophia', 'Serna', NULL, 'M', 3220202752, 'mariasoserna54@hotmail.com', 'Cll119No.182-13', 'Bogota DC', 5),
(52667112, 'CC', 'C', ' Erika', NULL, 'Mediorreal', 'Castro', 'M', 3041456384, 'emc2001@hotmail.com', 'Cra9No.74-99', 'Bogota DC', 9),
(52707628, 'CC', 'C', ' Sergio', 'Andres', 'Caicedo', 'Castro', 'H', 3004319232, 'sergioandrescc@misena.edu.co', 'Cll7No.68-91', 'Bogota DC', 1),
(53789789, 'CC', 'C', ' Edward', 'Julian', 'Patino', NULL, 'H', 3195420416, 'ejpatino2233@outlook.com', 'Cll2No.2-55', 'Bogota DC', 12),
(53958786, 'CC', 'C', ' Esteban', ' Adrian', 'Colmenares', 'Zea', 'H', 3195567360, 'estebancolmenareszea@gmail.com', 'Kra 153 No. 43-148', 'Bogota DC', 1),
(57772562, 'CC', 'V', ' Helena', 'Maria', 'Cruz', 'Cristancho', 'M', NULL, 'hmcristancho@micorreo.com', 'Kra15sur#45-20', 'Bogota DC', 20),
(58999866, 'CC', 'C', ' Edith ', '', 'Rincon', 'Suarez', 'M', 3219799808, 'edithrincon91@yahoo.com', 'Tv98No.20-57', 'Bogota DC', 10),
(195678439, 'CC', 'C', ' Emilio ', 'Jair', 'Woodstock', NULL, 'H', 3023490304, 'emiliowoodstock@gmail.com', 'Cra12No.8-61', 'Bogota DC', 11),
(1009952345, 'CC', 'C', ' Jeronimo ', NULL, 'Angel', 'Montano', 'H', 3114547200, 'jangelmontano@hotmail.com', 'Kra127No.24-91', 'Bogota DC', 5),
(1010432551, 'CC', 'C', ' Hair', 'Jose', 'Morales', NULL, 'H', 3102244352, 'haimorales@yahoo.com', 'Cra10No.10-104', 'Bogota DC', 16),
(1010454599, 'CC', 'C', ' Gabriel', 'Esteban', 'Carvajalino', 'Beltran', 'H', 3014511616, 'gecarvajalino@outlook.es', 'Cll48bSurNo.21-13', 'Bogota DC', 12),
(1010677511, 'CC', 'C', ' Edwin', NULL, 'Martinez', 'Contreras', 'H', 3016196864, 'eedmartinez@policianacional.gov.co', 'Cra20No.37-54', 'Bogota DC', 6),
(1010963144, 'CC', 'C', ' Nicolas', 'Andres', 'Benavides', 'Rodriguez', 'H', 3102238208, 'nicolasandresbrodriguez@gmail.com', 'Cra1ENo.3-38', 'Bogota DC', 13),
(1012002323, 'CC', 'C', ' Giovanny ', NULL, 'Angel', 'Moreno', 'H', 3206479360, 'ggangelm@gmail.com', 'Kra74No.82a-81', 'Bogota DC', 12),
(1013156744, 'CC', 'C', ' Jessica ', 'Johanna', 'Acevedo', 'Ortiz', 'M', 3048271104, 'jjacevedo12@yahoo.com', 'Cll11No.4-14', 'Bogota DC', 11),
(1014241788, 'CC', 'C', 'Sandra', 'yurany', 'Cuevas', 'Cruz', 'M', 3112238336, 'sandraycuevas@email.com', 'Kra12kNo.27-49', 'Bogota DC', 17),
(1014356714, 'CC', 'C', 'Diego', 'Andres', 'Torres', NULL, 'H', 3219799808, 'datorres410@misena.edu.co', 'Kra 20 sur # 5L - 10', 'Bogota DC', 6),
(1014567234, 'CC', 'C', ' Doris ', ' Estella', 'Jimenez', 'Castillo', 'M', 3141114624, 'dorisstella545@yahoo.com', 'Av 188 No. 1- 92', 'Bogota DC', 3),
(1015478578, 'CC', 'C', ' Jhon', 'Kevin', 'Poveda', 'Forero', 'H', 3102225408, 'kevinpovedaforero@hotmail.com', 'Cll20No.2-91Este', 'Bogota DC', 20),
(1015630525, 'CC', 'C', ' Felipe', NULL, 'Tafur', 'Alvarado', 'H', 3100013568, 'felipetafuralvarado@hotmail.com', 'Av19No.84-45', 'Bogota DC', 3),
(1015666732, 'CC', 'C', ' Jhon ', 'Camilo', 'Serrano', 'Cruz', 'H', 3112222976, 'jcamiloserranoc@yahoo.com', 'AvpradillaNo.5-57', 'Bogota DC', 12),
(1015998331, 'CC', 'C', ' Leidy', 'Alexandra', 'Patino', 'Franco', 'M', 3106715136, 'alexafranco89@gmail.com', 'Cll27surNo.22-19', 'Bogota DC', 3),
(1016334500, 'CC', 'C', ' Adriana', 'Camila', 'Castiblanco', 'Noguera', 'M', 3016097024, 'camilacastiblanco11@gmail.com', 'Cll3No.1-19Este', 'Bogota DC', 18),
(1020784566, 'CC', 'V', ' Jose', 'Jesus', 'Botero', 'Buitrago', 'H', 3224556800, 'jjbotero1992@micorreo.com', 'Cll27#22-98', 'Bogota DC', 2),
(1030305620, 'CC', 'C', ' Cristian', 'David', 'Duarte', '', 'H', 3219799808, 'cdavidduarte2019@email.com', 'Cll11No.4-14', 'Bogota DC', 5);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `condicion_articulos`
--

CREATE TABLE `condicion_articulos` (
  `id_condicion_articulo` varchar(2) NOT NULL,
  `nombre_condicionart` varchar(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `condicion_articulos`
--

INSERT INTO `condicion_articulos` (`id_condicion_articulo`, `nombre_condicionart`) VALUES
('EC', 'In Contract'),
('EV', 'On Sale'),
('V', 'For Sale');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `condicion_contratos`
--

CREATE TABLE `condicion_contratos` (
  `Id_condicion_contrato` varchar(3) NOT NULL,
  `Nombre_condicioncon` varchar(17) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `condicion_contratos`
--

INSERT INTO `condicion_contratos` (`Id_condicion_contrato`, `Nombre_condicioncon`) VALUES
('C', 'Concluded'),
('CSP', 'Without Payment'),
('V', 'Valid');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `condicion_legalidad`
--

CREATE TABLE `condicion_legalidad` (
  `CondicionLegalidad` varchar(2) NOT NULL,
  `Nombre_Condicion` varchar(12) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `condicion_legalidad`
--

INSERT INTO `condicion_legalidad` (`CondicionLegalidad`, `Nombre_Condicion`) VALUES
('CF', 'With Invoice'),
('SF', 'No Invoice');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contratos`
--

CREATE TABLE `contratos` (
  `No_contrato` varchar(10) NOT NULL,
  `Cedula_cliente` int(11) NOT NULL,
  `Cedula_usuario` int(11) NOT NULL,
  `Fecha_inicio` date NOT NULL,
  `Fecha_vencimiento` date NOT NULL,
  `Fecha_pago` date DEFAULT NULL,
  `Plazo_estipulado` int(2) NOT NULL,
  `Id_articulo` varchar(10) NOT NULL,
  `Id_condicion_contrato` varchar(3) NOT NULL,
  `No_prorroga` varchar(10) NOT NULL,
  `Valor_contrato` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `contratos`
--

INSERT INTO `contratos` (`No_contrato`, `Cedula_cliente`, `Cedula_usuario`, `Fecha_inicio`, `Fecha_vencimiento`, `Fecha_pago`, `Plazo_estipulado`, `Id_articulo`, `Id_condicion_contrato`, `No_prorroga`, `Valor_contrato`) VALUES
('CT-00000', 0, 1945678, '2019-12-02', '2020-01-02', '0220-11-10', 12, '0', 'C', 'PR-00000', 200000),
('CT-00001', 53958786, 48779564, '2019-12-01', '2020-01-01', NULL, 30, 'TYO00001', 'CSP', 'PR-00000', 210000),
('CT-00002', 1014567234, 48779564, '2019-12-30', '2020-01-30', NULL, 30, 'TYO00002', 'CSP', 'PR-00000', 350000),
('CT-00003', 52707628, 48779564, '2019-12-25', '2020-01-25', NULL, 30, 'REL00001', 'CSP', 'PR-00000', 100000),
('CT-00004', 1015630525, 48779564, '2019-12-19', '2020-01-19', NULL, 30, 'REL00002', 'CSP', 'PR-00000', 150000),
('CT-00005', 45223554, 48779564, '2019-11-11', '2019-12-11', NULL, 30, 'TYO00003', 'CSP', 'PR-00000', 80000),
('CT-00006', 37985123, 1945678, '2019-10-22', '2019-11-22', '2019-12-15', 30, 'TYO00004', 'C', 'PR-00001', 45000),
('CT-00007', 58999866, 1945678, '2019-10-18', '2019-11-18', '2019-12-18', 30, 'TYO00005', 'C', 'PR-00002', 89000),
('CT-00008', 195678439, 1945678, '2019-10-02', '2019-11-02', '2019-12-02', 30, 'TYO00006', 'C', 'PR-00003', 423500),
('CT-00009', 1010432551, 1945678, '2019-09-27', '2019-10-27', '2019-11-26', 30, 'TYO00007', 'C', 'PR-00004', 57800),
('CT-00010', 47123873, 1945678, '2019-09-14', '2019-10-14', '2019-11-12', 30, 'REL00003', 'C', 'PR-00005', 377000),
('CT-00011', 1015666732, 48779564, '2019-08-07', '2019-09-07', '2019-10-05', 30, 'ELE00001', 'C', 'PR-00006', 400100),
('CT-00012', 1009952345, 48779564, '2019-08-25', '2019-09-25', '2019-10-26', 30, 'ELE00002', 'C', 'PR-00007', 130000),
('CT-00013', 51889736, 48779564, '2019-08-19', '2019-09-19', '2019-10-20', 30, 'ELE00003', 'C', 'PR-00008', 80000),
('CT-00014', 41556345, 48779564, '2019-09-10', '2019-10-10', '2019-11-10', 30, 'ELE00004', 'C', 'PR-00009', 44000),
('CT-00015', 2756722, 48779564, '2019-10-21', '2019-11-21', '2019-12-22', 30, 'ELE00005', 'C', 'PR-00010', 205000),
('CT-00016', 2288911, 1945678, '2019-10-05', '2019-11-05', '2019-11-01', 30, 'TYO00008', 'C', 'PR-00000', 89000),
('CT-00017', 1010963144, 1945678, '2019-11-22', '2019-12-22', '2019-12-21', 30, 'TYO00009', 'C', 'PR-00000', 69360),
('CT-00018', 53789789, 1945678, '2019-11-18', '2019-12-18', '2019-12-15', 30, 'TYO00010', 'C', 'PR-00000', 48400),
('CT-00019', 1016334500, 1945678, '2019-12-13', '2020-01-13', '2020-01-11', 30, 'TYO00011', 'C', 'PR-00000', 88000),
('CT-00020', 35112376, 1945678, '2020-01-25', '2020-02-25', '2020-02-18', 30, 'TYO00012', 'C', 'PR-00000', 97900),
('CT-00021', 1014241788, 1945678, '2019-10-22', '2019-11-22', '2019-11-05', 30, 'CYV00001', 'C', 'PR-00000', 156000),
('CT-00022', 1015998331, 1945678, '2020-01-07', '2020-02-07', '2020-02-01', 30, 'CYV00002', 'C', 'PR-00000', 490100),
('CT-00023', 45123477, 1945678, '2019-12-02', '2020-01-02', '2019-12-30', 30, 'CYV00003', 'C', 'PR-00000', 110000),
('CT-00024', 1030305620, 48779564, '2020-06-24', '2020-07-24', NULL, 30, 'ELE00006', 'V', 'PR-00000', 72000),
('CT-00025', 1010677511, 48779564, '2020-07-15', '2020-08-15', NULL, 30, 'CYV00004', 'V', 'PR-00000', 75500),
('CT-00026', 52667112, 48779564, '2020-06-30', '2020-07-30', NULL, 30, 'JOY00001', 'V', 'PR-00000', 50000),
('CT-00027', 1010454599, 48779564, '2020-07-10', '2020-08-10', NULL, 30, 'JOY00002', 'V', 'PR-00000', 55000),
('CT-00028', 1012002323, 48779564, '2020-06-05', '2020-07-05', NULL, 30, 'JOY00003', 'V', 'PR-00000', 250000),
('CT-00029', 1013156744, 48779564, '2020-06-16', '2020-07-16', NULL, 30, 'JOY00004', 'V', 'PR-00000', 203000),
('CT-00030', 1015478578, 1945678, '2020-05-15', '2020-06-15', NULL, 30, 'JOY00005', 'V', 'PR-00000', 180000),
('CT-00031', 1030305620, 1014241792, '2020-07-22', '2020-08-22', '2020-07-21', 30, 'TYO00013', 'V', 'PR-00011', 480000),
('CT-00032', 1030305620, 1014241792, '2020-07-22', '2020-08-22', NULL, 30, 'JOY00006', 'V', 'PR-00000', 200000),
('CT-00033', 2288911, 1014241792, '2020-07-20', '2020-08-20', NULL, 30, 'ELE00007', 'V', 'PR-00000', 100000),
('CT-00034', 1014356714, 48779564, '2020-07-14', '2020-08-14', NULL, 30, 'REL00004', 'V', 'PR-00000', 120000);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `estado_articulos`
--

CREATE TABLE `estado_articulos` (
  `Id_estado_articulo` varchar(2) NOT NULL,
  `Nombre_estadoart` varchar(7) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `estado_articulos`
--

INSERT INTO `estado_articulos` (`Id_estado_articulo`, `Nombre_estadoart`) VALUES
('B', 'Good'),
('M', 'Poor'),
('R', 'Steady');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `genero`
--

CREATE TABLE `genero` (
  `idGenero` varchar(2) NOT NULL,
  `NombreGenero` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `genero`
--

INSERT INTO `genero` (`idGenero`, `NombreGenero`) VALUES
('H', 'Male'),
('M', 'Female');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inventario`
--

CREATE TABLE `inventario` (
  `Id_inventario` varchar(10) NOT NULL,
  `Cantidad` int(3) NOT NULL,
  `Id_articulo` varchar(10) NOT NULL,
  `Id_condicion_articulo` varchar(2) NOT NULL,
  `Id_categoria` varchar(4) NOT NULL,
  `Id_estado_articulo` varchar(2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `inventario`
--

INSERT INTO `inventario` (`Id_inventario`, `Cantidad`, `Id_articulo`, `Id_condicion_articulo`, `Id_categoria`, `Id_estado_articulo`) VALUES
('ESTAN1-1', 1, 'TYO00001', 'V', 'TYO', 'B'),
('ESTAN1-2', 1, 'TYO00002', 'V', 'TYO', 'B'),
('ESTAN1-3', 1, 'ELE00006', 'EC', 'ELE', 'B'),
('ESTAN1-4', 1, 'CYV00004', 'EC', 'CYV', 'B'),
('ESTAN1-5', 1, 'JOY00001', 'EC', 'JOY', 'B'),
('ESTAN1-6', 1, 'JOY00002', 'EC', 'JOY', 'B'),
('ESTAN2-1', 1, 'JOY00003', 'EC', 'JOY', 'B'),
('ESTAN2-2', 1, 'JOY00004', 'EC', 'JOY', 'B'),
('ESTAN2-3', 1, 'JOY00005', 'EC', 'JOY', 'B'),
('ESTAN2-4', 1, 'REL00001', 'EV', 'REL', 'B'),
('ESTAN2-5', 1, 'REL00002', 'EV', 'REL', 'B'),
('ESTAN2-6', 1, 'TYO00003', 'EV', 'TYO', 'R');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `legalidad_articulos`
--

CREATE TABLE `legalidad_articulos` (
  `Id_legalidad` int(3) NOT NULL,
  `CondLegalidad` varchar(2) NOT NULL,
  `Fecha_legalidad` date NOT NULL,
  `Cedula_cliente` int(11) NOT NULL,
  `Descripcion_legalidad` varchar(45) NOT NULL,
  `Id_articulo` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `legalidad_articulos`
--

INSERT INTO `legalidad_articulos` (`Id_legalidad`, `CondLegalidad`, `Fecha_legalidad`, `Cedula_cliente`, `Descripcion_legalidad`, `Id_articulo`) VALUES
(0, 'SF', '1900-01-01', 0, 'NN', '0'),
(1, 'CF', '2019-11-30', 53958786, ' Factura No 000034564 Ktronix', 'TYO00001'),
(2, 'SF', '2019-12-30', 1014567234, ' CONSTANCIA DE LEGALIDAD ANEXO AL CONTRATO', 'TYO00002'),
(3, 'CF', '2019-12-25', 52707628, 'Factura No 05677 Falabella', 'REL00001'),
(4, 'SF', '2019-12-19', 1015630525, 'CONSTANCIA DE LEGALIDAD ANEXO AL CONTRATO', 'REL00002'),
(5, 'SF', '2019-11-11', 45223554, 'CONSTANCIA DE LEGALIDAD ANEXO AL CONTRATO', 'TYO00003'),
(6, 'SF', '2019-10-22', 37985123, 'CONSTANCIA DE LEGALIDAD ANEXO AL CONTRATO', 'TYO00004'),
(7, 'SF', '2019-10-18', 58999866, 'CONSTANCIA DE LEGALIDAD ANEXO AL CONTRATO', 'TYO00005'),
(8, 'SF', '2019-10-02', 195678439, 'CONSTANCIA DE LEGALIDAD ANEXO AL CONTRATO', 'TYO00006'),
(9, 'CF', '2019-09-27', 1010432551, 'Factura No 1000002343 Easy', 'TYO00007'),
(10, 'CF', '2019-09-14', 47123873, 'Factura No. 00000045654321 Homecenter', 'REL00003'),
(11, 'CF', '2019-08-07', 1015666732, 'Factura No. 112332  Cencosud Almacenes Jumbo', 'ELE00001'),
(12, 'SF', '2019-08-25', 1009952345, 'CONSTANCIA DE LEGALIDAD ANEXO AL CONTRATO', 'ELE00002'),
(13, 'SF', '2019-08-19', 51889736, 'CONSTANCIA DE LEGALIDAD ANEXO AL CONTRATO', 'ELE00003'),
(14, 'SF', '2019-09-10', 41556345, 'CONSTANCIA DE LEGALIDAD ANEXO AL CONTRATO', 'ELE00004'),
(15, 'CF', '2019-10-21', 2756722, ' Factura No 000034997 Ktronix', 'ELE00005'),
(16, 'CF', '2019-10-05', 2288911, 'Factura No 05623 Falabella', 'TYO00008'),
(17, 'CF', '2019-11-22', 1010963144, 'Factura No 1000004678 Easy', 'TYO00009'),
(18, 'CF', '2019-11-18', 53789789, 'Factura No. 00000045636789 Homecenter', 'TYO00010'),
(19, 'SF', '2019-12-13', 1016334500, 'CONSTANCIA DE LEGALIDAD ANEXO AL CONTRATO', 'TYO00011'),
(20, 'SF', '2020-01-25', 35112376, 'CONSTANCIA DE LEGALIDAD ANEXO AL CONTRATO', 'TYO00012'),
(21, 'SF', '2019-10-22', 1014241788, 'CONSTANCIA DE LEGALIDAD ANEXO AL CONTRATO', 'CYV00001'),
(22, 'SF', '2020-01-07', 1015998331, 'CONSTANCIA DE LEGALIDAD ANEXO AL CONTRATO', 'CYV00002'),
(23, 'CF', '2019-12-02', 45123477, 'Factura No 05881 Falabella', 'CYV00003'),
(24, 'CF', '2020-03-08', 1030305620, 'Factura No. 114431  Cencosud Almacenes Metro', 'ELE00006'),
(25, 'CF', '2020-03-11', 1010677511, 'Factura No 09112 Falabella', 'CYV00004'),
(26, 'SF', '2020-03-20', 52667112, 'CONSTANCIA DE LEGALIDAD ANEXO AL CONTRATO', 'JOY00001'),
(27, 'SF', '2020-03-05', 1010454599, 'CONSTANCIA DE LEGALIDAD ANEXO AL CONTRATO', 'JOY00002'),
(28, 'CF', '2020-03-17', 1012002323, 'Factura No. 2343 Almacen La Perla', 'JOY00003'),
(29, 'CF', '2020-03-26', 1013156744, 'Factura No. 9912 Almacen Esmeralda', 'JOY00004'),
(30, 'CF', '2020-03-15', 1015478578, 'Factura No. 0007667 Kelvin\'s', 'JOY00005'),
(31, 'SF', '2020-07-22', 1030305620, 'CONSTANCIA DE LEGALIDAD ANEXO AL CONTRATO', 'TYO00013'),
(32, 'SF', '2020-07-01', 1030305620, 'CONSTANCIA DE LEGALIDAD ANEXO AL CONTRATO', 'JOY00006'),
(33, 'SF', '2020-07-20', 2288911, 'CONSTANCIA DE LEGALIDAD ANEXO AL CONTRATO', 'ELE00007'),
(34, 'SF', '2020-07-14', 1014356714, 'CONSTANCIA DE LEGALIDAD ANEXO AL CONTRATO', 'REL00004');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `localidad`
--

CREATE TABLE `localidad` (
  `Id_localidad` int(11) NOT NULL,
  `Nombre_localidad` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `localidad`
--

INSERT INTO `localidad` (`Id_localidad`, `Nombre_localidad`) VALUES
(0, 'NULL'),
(1, 'Usaquen'),
(2, 'Chapinero'),
(3, 'Santafe'),
(4, 'San Cristóbal'),
(5, 'Usme'),
(6, 'Tunjuelito'),
(7, 'Bosa'),
(8, 'Kennedy'),
(9, 'Fontibón'),
(10, 'Engativá'),
(11, 'Suba'),
(12, 'Barrios Unidos'),
(13, 'Teusaquillo'),
(14, 'Los Mártires'),
(15, 'Antonio Nariño'),
(16, 'Puente Aranda'),
(17, 'La Candelaria'),
(18, 'Rafael Uribe Uribe'),
(19, 'Ciudad Bolívar'),
(20, 'Sumapaz');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `prorrogas`
--

CREATE TABLE `prorrogas` (
  `No_prorroga` varchar(10) NOT NULL,
  `No_contrato` varchar(10) NOT NULL,
  `Fecha_inicio_prorroga` date NOT NULL,
  `Fecha_vencimiento_prorroga` date NOT NULL,
  `Meses_a_pagar` int(2) NOT NULL,
  `Valor_mes` int(11) NOT NULL,
  `Dias_vencidos` int(4) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `prorrogas`
--

INSERT INTO `prorrogas` (`No_prorroga`, `No_contrato`, `Fecha_inicio_prorroga`, `Fecha_vencimiento_prorroga`, `Meses_a_pagar`, `Valor_mes`, `Dias_vencidos`) VALUES
('PR-00000', 'CT-00000', '1900-01-01', '1900-01-01', 0, 0, 0),
('PR-00001', 'CT-00006', '2019-11-23', '2019-12-23', 1, 49500, 23),
('PR-00002', 'CT-00007', '2019-12-19', '2020-01-19', 1, 97900, 30),
('PR-00003', 'CT-00008', '2020-01-03', '2020-02-03', 1, 465850, 30),
('PR-00004', 'CT-00009', '2019-10-28', '2019-11-28', 1, 69360, 30),
('PR-00005', 'CT-00010', '2019-12-15', '2020-01-15', 1, 490100, 29),
('PR-00006', 'CT-00011', '2019-10-08', '2019-11-08', 1, 520130, 28),
('PR-00007', 'CT-00012', '2019-09-26', '2019-10-26', 1, 156000, 31),
('PR-00008', 'CT-00013', '2019-10-20', '2019-11-20', 1, 88000, 31),
('PR-00009', 'CT-00014', '2019-11-11', '2019-12-11', 1, 48400, 31),
('PR-00010', 'CT-00015', '2019-11-22', '2019-12-22', 1, 225500, 31),
('PR-00011', 'CT-00031', '2020-08-23', '2020-09-23', 1, 120000, 12);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipo_cliente`
--

CREATE TABLE `tipo_cliente` (
  `Id_tipocliente` varchar(2) NOT NULL,
  `Nombre_tipocliente` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `tipo_cliente`
--

INSERT INTO `tipo_cliente` (`Id_tipocliente`, `Nombre_tipocliente`) VALUES
('C', 'Contracts '),
('V', 'Sales');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipo_documento`
--

CREATE TABLE `tipo_documento` (
  `Id_tipo_documento` varchar(2) NOT NULL,
  `Tipo_documento` varchar(22) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `tipo_documento`
--

INSERT INTO `tipo_documento` (`Id_tipo_documento`, `Tipo_documento`) VALUES
('CC', 'Identification Card'),
('CE', 'Foreigner Id'),
('N', 'NULL');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipo_usuario`
--

CREATE TABLE `tipo_usuario` (
  `Id_tipo_usuario` varchar(2) NOT NULL,
  `Nombre_tipous` varchar(15) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `tipo_usuario`
--

INSERT INTO `tipo_usuario` (`Id_tipo_usuario`, `Nombre_tipous`) VALUES
('A', 'Administrator'),
('C', 'Customer'),
('E', 'Employee');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios_sistema`
--

CREATE TABLE `usuarios_sistema` (
  `Cedula_usuario` int(11) NOT NULL,
  `Id_documento` varchar(2) NOT NULL,
  `Id_usuario` varchar(2) NOT NULL,
  `Nombreusuario1` varchar(15) NOT NULL,
  `Nombreusuario2` varchar(15) DEFAULT NULL,
  `Apellidousuario1` varchar(15) NOT NULL,
  `Apellidousuario2` varchar(15) DEFAULT NULL,
  `Email` varchar(35) NOT NULL,
  `Usuario` varchar(15) NOT NULL,
  `clave` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `usuarios_sistema`
--

INSERT INTO `usuarios_sistema` (`Cedula_usuario`, `Id_documento`, `Id_usuario`, `Nombreusuario1`, `Nombreusuario2`, `Apellidousuario1`, `Apellidousuario2`, `Email`, `Usuario`, `clave`) VALUES
(1945678, 'CC', 'E', 'Jose', 'Andres', 'Arenas ', 'Martinez', 'cristiandelgadillo21@gmail.com', 'jarenasm', '99834321'),
(48779564, 'CC', 'A', 'William', '', 'Delgadillo', 'Duitama', 'e1506@esave.com.co', 'wdduitama', '12345+Sp'),
(53958786, 'CC', 'C', 'Esteban', 'Adrian', 'Colmenares', 'Zea', 'bgortiz92@misena.edu.co', 'ecolmenaresz', 'Bogota1221'),
(1014241792, 'CC', 'A', 'Samanta', 'Geraldine', 'Hidalgo', '', 'sghidalgo@misena.edu.co', 'Shidalgo', 'sgh12345'),
(1030690573, 'CC', 'E', 'christian', '', 'delgadillo', '', 'ccddcristian@gmail.com', 'cristian', '980721Ac.');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ventas`
--

CREATE TABLE `ventas` (
  `No_factura` varchar(10) NOT NULL,
  `Fecha_factura` date NOT NULL,
  `Precio_unitario` int(11) NOT NULL,
  `Cantidad` int(3) NOT NULL,
  `SUB_TOTAL` int(11) NOT NULL,
  `IVA` float NOT NULL,
  `VALOR_TOTAL` int(11) NOT NULL,
  `Cedula_cliente` int(11) NOT NULL,
  `Cedula_usuario` int(11) NOT NULL,
  `Id_articulo` varchar(10) NOT NULL,
  `Id_estado_articulo` varchar(2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `ventas`
--

INSERT INTO `ventas` (`No_factura`, `Fecha_factura`, `Precio_unitario`, `Cantidad`, `SUB_TOTAL`, `IVA`, `VALOR_TOTAL`, `Cedula_cliente`, `Cedula_usuario`, `Id_articulo`, `Id_estado_articulo`) VALUES
('FV-00001', '2020-04-15', 250000, 1, 250000, 0.02, 255000, 57772562, 48779564, 'TYO00001', 'B'),
('FV-00002', '2020-04-16', 280000, 1, 280000, 0, 380000, 1020784566, 48779564, 'TYO00002', 'B');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `articulos`
--
ALTER TABLE `articulos`
  ADD PRIMARY KEY (`IdArticulo`),
  ADD KEY `fk_estadoarti_idx` (`Id_estado_articulo`),
  ADD KEY `fk_categoria_idx` (`Id_categoria`),
  ADD KEY `fk_genero_idx` (`Genero`);

--
-- Indices de la tabla `categorias`
--
ALTER TABLE `categorias`
  ADD PRIMARY KEY (`Id_categoria`);

--
-- Indices de la tabla `clientes`
--
ALTER TABLE `clientes`
  ADD PRIMARY KEY (`Cedula_cliente`),
  ADD KEY `fk_localidad_idx` (`Id_localidad`),
  ADD KEY `fk_tdocumento_idx` (`Id_documento`),
  ADD KEY `fk_tcliente_idx` (`Id_tipocliente`),
  ADD KEY `fk_genero_idx` (`Genero`);

--
-- Indices de la tabla `condicion_articulos`
--
ALTER TABLE `condicion_articulos`
  ADD PRIMARY KEY (`id_condicion_articulo`);

--
-- Indices de la tabla `condicion_contratos`
--
ALTER TABLE `condicion_contratos`
  ADD PRIMARY KEY (`Id_condicion_contrato`);

--
-- Indices de la tabla `condicion_legalidad`
--
ALTER TABLE `condicion_legalidad`
  ADD PRIMARY KEY (`CondicionLegalidad`);

--
-- Indices de la tabla `contratos`
--
ALTER TABLE `contratos`
  ADD PRIMARY KEY (`No_contrato`),
  ADD KEY `fk_ccliente_idx` (`Cedula_cliente`),
  ADD KEY `fk_cusuario_idx` (`Cedula_usuario`),
  ADD KEY `fk_ccontrato_idx` (`Id_condicion_contrato`),
  ADD KEY `fk_narticulo_idx` (`Id_articulo`),
  ADD KEY `fk_nprorrogas_idx` (`No_prorroga`);

--
-- Indices de la tabla `estado_articulos`
--
ALTER TABLE `estado_articulos`
  ADD PRIMARY KEY (`Id_estado_articulo`);

--
-- Indices de la tabla `genero`
--
ALTER TABLE `genero`
  ADD PRIMARY KEY (`idGenero`);

--
-- Indices de la tabla `inventario`
--
ALTER TABLE `inventario`
  ADD PRIMARY KEY (`Id_inventario`),
  ADD KEY `fk_esart_idx` (`Id_estado_articulo`),
  ADD KEY `fk_conarti_idx` (`Id_condicion_articulo`),
  ADD KEY `fk_cate_idx` (`Id_categoria`),
  ADD KEY `fk_idarticulo_idx` (`Id_articulo`);

--
-- Indices de la tabla `legalidad_articulos`
--
ALTER TABLE `legalidad_articulos`
  ADD PRIMARY KEY (`Id_legalidad`),
  ADD KEY `fk_idcliente_idx` (`Cedula_cliente`),
  ADD KEY `fk_conlega_idx` (`CondLegalidad`),
  ADD KEY `fk_numarti_idx` (`Id_articulo`);

--
-- Indices de la tabla `localidad`
--
ALTER TABLE `localidad`
  ADD PRIMARY KEY (`Id_localidad`);

--
-- Indices de la tabla `prorrogas`
--
ALTER TABLE `prorrogas`
  ADD PRIMARY KEY (`No_prorroga`),
  ADD KEY `fk_ncontrato_idx` (`No_contrato`);

--
-- Indices de la tabla `tipo_cliente`
--
ALTER TABLE `tipo_cliente`
  ADD PRIMARY KEY (`Id_tipocliente`);

--
-- Indices de la tabla `tipo_documento`
--
ALTER TABLE `tipo_documento`
  ADD PRIMARY KEY (`Id_tipo_documento`);

--
-- Indices de la tabla `tipo_usuario`
--
ALTER TABLE `tipo_usuario`
  ADD PRIMARY KEY (`Id_tipo_usuario`);

--
-- Indices de la tabla `usuarios_sistema`
--
ALTER TABLE `usuarios_sistema`
  ADD PRIMARY KEY (`Cedula_usuario`),
  ADD KEY `fk_usuariotipo_idx` (`Id_usuario`),
  ADD KEY `fk_tipodoc_idx` (`Id_documento`);

--
-- Indices de la tabla `ventas`
--
ALTER TABLE `ventas`
  ADD PRIMARY KEY (`No_factura`),
  ADD KEY `fk_cedulacl_idx` (`Cedula_cliente`),
  ADD KEY `fk_cedulaus_idx` (`Cedula_usuario`),
  ADD KEY `fk_estadoa_idx` (`Id_estado_articulo`),
  ADD KEY `fk_artinum_idx` (`Id_articulo`);

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `articulos`
--
ALTER TABLE `articulos`
  ADD CONSTRAINT `fk_artgene` FOREIGN KEY (`Genero`) REFERENCES `genero` (`idGenero`),
  ADD CONSTRAINT `fk_categoria` FOREIGN KEY (`Id_categoria`) REFERENCES `categorias` (`Id_categoria`),
  ADD CONSTRAINT `fk_estadoarti` FOREIGN KEY (`Id_estado_articulo`) REFERENCES `estado_articulos` (`Id_estado_articulo`);

--
-- Filtros para la tabla `clientes`
--
ALTER TABLE `clientes`
  ADD CONSTRAINT `fk_documentoti` FOREIGN KEY (`Id_documento`) REFERENCES `tipo_documento` (`Id_tipo_documento`),
  ADD CONSTRAINT `fk_genero` FOREIGN KEY (`Genero`) REFERENCES `genero` (`idGenero`),
  ADD CONSTRAINT `fk_localidad` FOREIGN KEY (`Id_localidad`) REFERENCES `localidad` (`Id_localidad`),
  ADD CONSTRAINT `fk_tcliente` FOREIGN KEY (`Id_tipocliente`) REFERENCES `tipo_cliente` (`Id_tipocliente`);

--
-- Filtros para la tabla `contratos`
--
ALTER TABLE `contratos`
  ADD CONSTRAINT `fk_ccliente` FOREIGN KEY (`Cedula_cliente`) REFERENCES `clientes` (`Cedula_cliente`),
  ADD CONSTRAINT `fk_ccontrato` FOREIGN KEY (`Id_condicion_contrato`) REFERENCES `condicion_contratos` (`Id_condicion_contrato`),
  ADD CONSTRAINT `fk_cusuario` FOREIGN KEY (`Cedula_usuario`) REFERENCES `usuarios_sistema` (`Cedula_usuario`),
  ADD CONSTRAINT `fk_narticulo` FOREIGN KEY (`Id_articulo`) REFERENCES `articulos` (`IdArticulo`),
  ADD CONSTRAINT `fk_nprorroga` FOREIGN KEY (`No_prorroga`) REFERENCES `prorrogas` (`No_prorroga`);

--
-- Filtros para la tabla `inventario`
--
ALTER TABLE `inventario`
  ADD CONSTRAINT `fk_cate` FOREIGN KEY (`Id_categoria`) REFERENCES `categorias` (`Id_categoria`),
  ADD CONSTRAINT `fk_conarti` FOREIGN KEY (`Id_condicion_articulo`) REFERENCES `condicion_articulos` (`id_condicion_articulo`),
  ADD CONSTRAINT `fk_esart` FOREIGN KEY (`Id_estado_articulo`) REFERENCES `estado_articulos` (`Id_estado_articulo`),
  ADD CONSTRAINT `fk_idarticulo` FOREIGN KEY (`Id_articulo`) REFERENCES `articulos` (`IdArticulo`);

--
-- Filtros para la tabla `legalidad_articulos`
--
ALTER TABLE `legalidad_articulos`
  ADD CONSTRAINT `fk_conlega` FOREIGN KEY (`CondLegalidad`) REFERENCES `condicion_legalidad` (`CondicionLegalidad`),
  ADD CONSTRAINT `fk_idcliente` FOREIGN KEY (`Cedula_cliente`) REFERENCES `clientes` (`Cedula_cliente`),
  ADD CONSTRAINT `fk_numarti` FOREIGN KEY (`Id_articulo`) REFERENCES `articulos` (`IdArticulo`);

--
-- Filtros para la tabla `prorrogas`
--
ALTER TABLE `prorrogas`
  ADD CONSTRAINT `fk_ncontrato` FOREIGN KEY (`No_contrato`) REFERENCES `contratos` (`No_contrato`);

--
-- Filtros para la tabla `usuarios_sistema`
--
ALTER TABLE `usuarios_sistema`
  ADD CONSTRAINT `fk_tipodoc` FOREIGN KEY (`Id_documento`) REFERENCES `tipo_documento` (`Id_tipo_documento`),
  ADD CONSTRAINT `fk_tipous` FOREIGN KEY (`Id_usuario`) REFERENCES `tipo_usuario` (`Id_tipo_usuario`);

--
-- Filtros para la tabla `ventas`
--
ALTER TABLE `ventas`
  ADD CONSTRAINT `fk_artinum` FOREIGN KEY (`Id_articulo`) REFERENCES `articulos` (`IdArticulo`),
  ADD CONSTRAINT `fk_cedulacl` FOREIGN KEY (`Cedula_cliente`) REFERENCES `clientes` (`Cedula_cliente`),
  ADD CONSTRAINT `fk_cedulaus` FOREIGN KEY (`Cedula_usuario`) REFERENCES `usuarios_sistema` (`Cedula_usuario`),
  ADD CONSTRAINT `fk_estadoa` FOREIGN KEY (`Id_estado_articulo`) REFERENCES `estado_articulos` (`Id_estado_articulo`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
