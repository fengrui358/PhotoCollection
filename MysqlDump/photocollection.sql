-- MySQL dump 10.13  Distrib 5.6.16, for Win32 (x86)
--
-- Host: localhost    Database: photocollection
-- ------------------------------------------------------
-- Server version	5.6.16-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `photoinfos`
--

DROP TABLE IF EXISTS `photoinfos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `photoinfos` (
  `Md5` varchar(64) NOT NULL,
  `OrderNumber` bigint(20) NOT NULL AUTO_INCREMENT,
  `Url` varchar(500) DEFAULT NULL,
  `Type` varchar(50) DEFAULT NULL,
  `Size` bigint(20) DEFAULT NULL COMMENT '图片的尺寸大小',
  `Model` varchar(50) DEFAULT NULL COMMENT '设备型号',
  `GPSLatitude` varchar(50) DEFAULT NULL COMMENT '原始维度',
  `GPSLongitude` varchar(50) DEFAULT NULL COMMENT '原始经度',
  `BDLatitude` varchar(50) DEFAULT NULL COMMENT '百度维度',
  `BDLongitude` varchar(50) DEFAULT NULL COMMENT '百度经度',
  `Address` varchar(256) DEFAULT NULL COMMENT '地址描述',
  `Country` varchar(50) DEFAULT NULL COMMENT '国家',
  `Province` varchar(50) DEFAULT NULL COMMENT '省份',
  `City` varchar(50) DEFAULT NULL COMMENT '城市',
  `District` varchar(50) DEFAULT NULL COMMENT '行政区',
  `Street` varchar(50) DEFAULT NULL COMMENT '街道',
  `Exif` varchar(20000) DEFAULT NULL COMMENT '从七牛云获得的exif信息',
  `CreateTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Md5`,`OrderNumber`),
  UNIQUE KEY `OrderNumber` (`OrderNumber`)
) ENGINE=MyISAM AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-06-21 16:28:06
