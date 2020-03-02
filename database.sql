-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: localhost    Database: smproject
-- ------------------------------------------------------
-- Server version	8.0.19

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `tblartikel`
--

DROP TABLE IF EXISTS `tblartikel`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tblartikel` (
  `productnr` int NOT NULL AUTO_INCREMENT,
  `productnaam` text NOT NULL,
  `prijs` int NOT NULL,
  `stock` int NOT NULL,
  `winkelnr` int NOT NULL,
  `korting` int NOT NULL,
  `actief` int NOT NULL,
  PRIMARY KEY (`productnr`)
) ENGINE=MyISAM AUTO_INCREMENT=43 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tblartikel`
--

LOCK TABLES `tblartikel` WRITE;
/*!40000 ALTER TABLE `tblartikel` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblartikel` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tblbesteldeartikels`
--

DROP TABLE IF EXISTS `tblbesteldeartikels`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tblbesteldeartikels` (
  `bestelnr` int NOT NULL,
  `productnr` int NOT NULL,
  `prijs` int NOT NULL,
  PRIMARY KEY (`bestelnr`,`productnr`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tblbesteldeartikels`
--

LOCK TABLES `tblbesteldeartikels` WRITE;
/*!40000 ALTER TABLE `tblbesteldeartikels` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblbesteldeartikels` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tblbestelling`
--

DROP TABLE IF EXISTS `tblbestelling`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tblbestelling` (
  `bestelnr` int NOT NULL AUTO_INCREMENT,
  `datum` date NOT NULL,
  `gebruikersnaam` varchar(40) NOT NULL,
  `prijs` int NOT NULL,
  PRIMARY KEY (`bestelnr`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tblbestelling`
--

LOCK TABLES `tblbestelling` WRITE;
/*!40000 ALTER TABLE `tblbestelling` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblbestelling` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tblinschrijvingen`
--

DROP TABLE IF EXISTS `tblinschrijvingen`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tblinschrijvingen` (
  `id` int NOT NULL AUTO_INCREMENT,
  `naam` varchar(75) NOT NULL,
  `klas` varchar(10) NOT NULL,
  `gast1` varchar(75) NOT NULL,
  `gast2` varchar(75) NOT NULL,
  `bevestigdGastheer` int NOT NULL,
  `bevestigdGast1` int NOT NULL,
  `bevestigdGast2` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tblinschrijvingen`
--

LOCK TABLES `tblinschrijvingen` WRITE;
/*!40000 ALTER TABLE `tblinschrijvingen` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblinschrijvingen` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tblrekening`
--

DROP TABLE IF EXISTS `tblrekening`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tblrekening` (
  `gebruikersnaam` varchar(45) NOT NULL,
  `krediet` double NOT NULL,
  PRIMARY KEY (`gebruikersnaam`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tblrekening`
--

LOCK TABLES `tblrekening` WRITE;
/*!40000 ALTER TABLE `tblrekening` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblrekening` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbltransacties`
--

DROP TABLE IF EXISTS `tbltransacties`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbltransacties` (
  `transactienr` int NOT NULL AUTO_INCREMENT,
  `datum` date NOT NULL,
  `bedrag` double NOT NULL,
  `gebruikersnaam` varchar(45) NOT NULL,
  `omschrijving` varchar(150) NOT NULL,
  PRIMARY KEY (`transactienr`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbltransacties`
--

LOCK TABLES `tbltransacties` WRITE;
/*!40000 ALTER TABLE `tbltransacties` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbltransacties` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tblwinkel`
--

DROP TABLE IF EXISTS `tblwinkel`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tblwinkel` (
  `winkelnr` int NOT NULL AUTO_INCREMENT,
  `naam` varchar(60) NOT NULL,
  `beheerder` varchar(45) NOT NULL,
  `actief` int NOT NULL,
  `goedgekeurd` int NOT NULL,
  PRIMARY KEY (`winkelnr`)
) ENGINE=MyISAM AUTO_INCREMENT=24 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tblwinkel`
--

LOCK TABLES `tblwinkel` WRITE;
/*!40000 ALTER TABLE `tblwinkel` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblwinkel` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-03-02 16:15:23
