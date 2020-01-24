-- phpMyAdmin SQL Dump
-- version 4.7.9
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Gegenereerd op: 20 jan 2020 om 14:24
-- Serverversie: 5.7.21
-- PHP-versie: 5.6.35

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `smproject`
--

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `tblartikel`
--

DROP TABLE IF EXISTS `tblartikel`;
CREATE TABLE IF NOT EXISTS `tblartikel` (
  `productnr` int(11) NOT NULL AUTO_INCREMENT,
  `productnaam` text NOT NULL,
  `prijs` int(11) NOT NULL,
  `stock` int(11) NOT NULL,
  `winkelnr` int(11) NOT NULL,
  `korting` int(11) NOT NULL,
  `actief` int(11) NOT NULL,
  PRIMARY KEY (`productnr`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `tblbesteldeartikels`
--

DROP TABLE IF EXISTS `tblbesteldeartikels`;
CREATE TABLE IF NOT EXISTS `tblbesteldeartikels` (
  `bestelnr` int(11) NOT NULL,
  `productnr` int(11) NOT NULL,
  `prijs` int(11) NOT NULL,
  PRIMARY KEY (`bestelnr`,`productnr`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `tblbestelling`
--

DROP TABLE IF EXISTS `tblbestelling`;
CREATE TABLE IF NOT EXISTS `tblbestelling` (
  `bestelnr` int(11) NOT NULL AUTO_INCREMENT,
  `datum` date NOT NULL,
  `gebruikersnaam` varchar(40) NOT NULL,
  `prijs` int(11) NOT NULL,
  PRIMARY KEY (`bestelnr`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `tblinventarisitems`
--

DROP TABLE IF EXISTS `tblinventarisitems`;
CREATE TABLE IF NOT EXISTS `tblinventarisitems` (
  `inventarisnr` int(11) NOT NULL AUTO_INCREMENT,
  `item` int(11) NOT NULL,
  PRIMARY KEY (`inventarisnr`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `tblitemsinlokalen`
--

DROP TABLE IF EXISTS `tblitemsinlokalen`;
CREATE TABLE IF NOT EXISTS `tblitemsinlokalen` (
  `lokaalnr` int(11) NOT NULL,
  `inventarisnr` int(11) NOT NULL,
  `aantal` int(11) NOT NULL,
  PRIMARY KEY (`lokaalnr`,`inventarisnr`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `tbllokaal`
--

DROP TABLE IF EXISTS `tbllokaal`;
CREATE TABLE IF NOT EXISTS `tbllokaal` (
  `lokaalnr` int(11) NOT NULL,
  `lokaalverantwoordelijke` varchar(40) NOT NULL,
  PRIMARY KEY (`lokaalnr`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `tblrekening`
--

DROP TABLE IF EXISTS `tblrekening`;
CREATE TABLE IF NOT EXISTS `tblrekening` (
  `gebruikersnaam` varchar(40) NOT NULL,
  `krediet` double(11) NOT NULL,
  PRIMARY KEY (`gebruikersnaam`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `tbltransacties`
--

DROP TABLE IF EXISTS `tbltransacties`;
CREATE TABLE IF NOT EXISTS `tbltransacties` (
  `transactienr` int(11) NOT NULL AUTO_INCREMENT,
  `datum` date NOT NULL,
  `bedrag` int(11) NOT NULL,
  `gebruikersnaam` varchar(40) NOT NULL,
  `omschrijving` text NOT NULL,
  PRIMARY KEY (`transactienr`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `tblwinkel`
--

DROP TABLE IF EXISTS `tblwinkel`;
CREATE TABLE IF NOT EXISTS `tblwinkel` (
  `winkelnr` int(11) NOT NULL AUTO_INCREMENT,
  `naam` text NOT NULL,
  `beheerder` varchar(40) NOT NULL,
  `actief` int(11) NOT NULL,
  PRIMARY KEY (`winkelnr`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
