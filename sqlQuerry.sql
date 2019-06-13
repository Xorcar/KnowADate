-- MySQL dump 10.13  Distrib 8.0.13, for Win64 (x86_64)
--
-- Host: localhost    Database: knowadate
-- ------------------------------------------------------
-- Server version	8.0.13

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Temporary view structure for view `eventinfo`
--

DROP TABLE IF EXISTS `eventinfo`;
/*!50001 DROP VIEW IF EXISTS `eventinfo`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8mb4;
/*!50001 CREATE VIEW `eventinfo` AS SELECT 
 1 AS `id`,
 1 AS `name`,
 1 AS `year`,
 1 AS `dif`,
 1 AS `ua`,
 1 AS `ru`,
 1 AS `pack`,
 1 AS `picture`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `eventnames`
--

DROP TABLE IF EXISTS `eventnames`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `eventnames` (
  `idEvent` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `ukrainian` varchar(45) NOT NULL,
  `russian` varchar(45) NOT NULL,
  PRIMARY KEY (`idEvent`),
  UNIQUE KEY `idEvent_UNIQUE` (`idEvent`),
  CONSTRAINT `eventId` FOREIGN KEY (`idEvent`) REFERENCES `events` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=53 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `eventnames`
--

LOCK TABLES `eventnames` WRITE;
/*!40000 ALTER TABLE `eventnames` DISABLE KEYS */;
INSERT INTO `eventnames` VALUES (1,'Відкриття Колумбом Америки','Открытые Колумбом Америки'),(39,'asdf','asdf'),(40,'Crucifixion of Jesus','Crucifixion of Jesus'),(41,'Start of World War II','Start of World War II'),(42,'Start of World War II','Start of World War II'),(43,'End of World War II','End of World War II'),(44,'End of World War II','End of World War II'),(45,'Watt\'s steam engine','Watt\'s steam engine'),(46,'Reis\' telephone','Reis\' telephone'),(47,'Death of Julius Cesar','Death of Julius Cesar'),(48,'First ancient Olympic game','First ancient Olympic game'),(49,'Ukraine Independence Day','Ukraine Independence Day'),(50,'The Constitution of Ukraine','The Constitution of Ukraine'),(51,'Orange Revolution (Ukraine)','Orange Revolution (Ukraine)'),(52,'Union of Brest','Union of Brest');
/*!40000 ALTER TABLE `eventnames` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `events`
--

DROP TABLE IF EXISTS `events`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `events` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `year` int(11) NOT NULL,
  `difficulty` int(11) NOT NULL DEFAULT '5',
  PRIMARY KEY (`id`),
  UNIQUE KEY `name_UNIQUE` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=53 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `events`
--

LOCK TABLES `events` WRITE;
/*!40000 ALTER TABLE `events` DISABLE KEYS */;
INSERT INTO `events` VALUES (1,'1312',2134,5),(39,'Columb\'s discovery of America',1492,5),(40,'Crucifixion of Jesus',33,5),(41,'Start of World War II',1939,5),(42,'Start of World War I',1914,5),(43,'End of World War II',1945,5),(44,'End of World War I',1918,5),(45,'Watt\'s steam engine',1781,5),(46,'Reis\' telephone',1860,5),(47,'Death of Julius Cesar',-44,5),(48,'First ancient Olympic game',-776,5),(49,'Ukraine Independence Day',1991,2),(50,'The Constitution of Ukraine',1996,2),(51,'Orange Revolution (Ukraine)',2004,3),(52,'Union of Brest',1596,8);
/*!40000 ALTER TABLE `events` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `eventshaspacks`
--

DROP TABLE IF EXISTS `eventshaspacks`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `eventshaspacks` (
  `idEvents` int(10) unsigned NOT NULL,
  `idPack` int(10) unsigned NOT NULL,
  PRIMARY KEY (`idEvents`),
  UNIQUE KEY `idEvents_UNIQUE` (`idEvents`),
  KEY `packId_idx` (`idPack`),
  CONSTRAINT `eventId4Pack` FOREIGN KEY (`idEvents`) REFERENCES `events` (`id`) ON DELETE CASCADE,
  CONSTRAINT `packId` FOREIGN KEY (`idPack`) REFERENCES `packs` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `eventshaspacks`
--

LOCK TABLES `eventshaspacks` WRITE;
/*!40000 ALTER TABLE `eventshaspacks` DISABLE KEYS */;
INSERT INTO `eventshaspacks` VALUES (39,5),(40,5),(41,5),(42,5),(43,5),(44,5),(45,5),(46,5),(47,5),(48,5),(49,6),(50,6),(51,6),(52,6);
/*!40000 ALTER TABLE `eventshaspacks` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `eventshaspictures`
--

DROP TABLE IF EXISTS `eventshaspictures`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `eventshaspictures` (
  `idEvent` int(10) unsigned NOT NULL,
  `idPicture` int(10) unsigned NOT NULL DEFAULT '1',
  PRIMARY KEY (`idEvent`),
  KEY `pictureId_idx` (`idPicture`),
  KEY `eventIdKey_idx` (`idEvent`),
  CONSTRAINT `eventIdKey` FOREIGN KEY (`idEvent`) REFERENCES `events` (`id`),
  CONSTRAINT `pictureIdKey` FOREIGN KEY (`idPicture`) REFERENCES `pictures` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `eventshaspictures`
--

LOCK TABLES `eventshaspictures` WRITE;
/*!40000 ALTER TABLE `eventshaspictures` DISABLE KEYS */;
INSERT INTO `eventshaspictures` VALUES (1,1),(49,1),(50,1),(51,1),(52,1),(39,23),(40,24),(41,25),(43,25),(42,26),(44,26),(45,27),(46,28),(47,29),(48,30);
/*!40000 ALTER TABLE `eventshaspictures` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `packs`
--

DROP TABLE IF EXISTS `packs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `packs` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  UNIQUE KEY `name_UNIQUE` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `packs`
--

LOCK TABLES `packs` WRITE;
/*!40000 ALTER TABLE `packs` DISABLE KEYS */;
INSERT INTO `packs` VALUES (5,'General'),(6,'Ukraine');
/*!40000 ALTER TABLE `packs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pictures`
--

DROP TABLE IF EXISTS `pictures`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `pictures` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `path` varchar(100) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pictures`
--

LOCK TABLES `pictures` WRITE;
/*!40000 ALTER TABLE `pictures` DISABLE KEYS */;
INSERT INTO `pictures` VALUES (1,'default_picture.jpg'),(23,'220px-Landing_of_Columbus.jpg'),(24,'240px-SVouet.jpg'),(25,'300px-Infobox_collage_for_WWII.PNG'),(26,'WWImontage.jpg'),(27,'220px-Grazebrook_Beam_Engine.jpg'),(28,'220px-DBP_1984_1198_Philipp_Reis.jpg'),(29,'César_(13667960455).jpg'),(30,'152px-Stamp_of_Greece._1896_Olympic_Games._2l.jpg');
/*!40000 ALTER TABLE `pictures` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `players`
--

DROP TABLE IF EXISTS `players`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `players` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `points` int(11) NOT NULL,
  `password` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `players`
--

LOCK TABLES `players` WRITE;
/*!40000 ALTER TABLE `players` DISABLE KEYS */;
INSERT INTO `players` VALUES (1,'Administrator',1,'1234'),(4,'Yurii Mykoliuk',162,'1111'),(5,'Max Hladiy',117,'1111'),(6,'Yurii Kuharuk',110,'1111');
/*!40000 ALTER TABLE `players` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Final view structure for view `eventinfo`
--

/*!50001 DROP VIEW IF EXISTS `eventinfo`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `eventinfo` AS select `events`.`id` AS `id`,`events`.`name` AS `name`,`events`.`year` AS `year`,`events`.`difficulty` AS `dif`,`eventnames`.`ukrainian` AS `ua`,`eventnames`.`russian` AS `ru`,`packs`.`name` AS `pack`,`pictures`.`path` AS `picture` from (((((`events` join `eventnames`) join `packs`) join `eventshaspacks`) join `eventshaspictures`) join `pictures`) where (((`events`.`id` = `eventnames`.`idEvent`) or isnull(`eventnames`.`idEvent`)) and (`eventshaspacks`.`idEvents` = `events`.`id`) and (`eventshaspacks`.`idPack` = `packs`.`id`) and (`eventshaspictures`.`idEvent` = `events`.`id`) and (`pictures`.`id` = `eventshaspictures`.`idPicture`)) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-06-13 21:25:02
