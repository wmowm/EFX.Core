/*
SQLyog Ultimate v12.09 (64 bit)
MySQL - 5.7.23 : Database - dhm
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`dhm` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `dhm`;

/*Table structure for table `Manager` */

DROP TABLE IF EXISTS `Manager`;

CREATE TABLE `Manager` (
  `Id` varchar(255) NOT NULL,
  `UserName` varchar(255) DEFAULT NULL,
  `Password` varchar(255) DEFAULT NULL,
  `Mobile` varchar(255) DEFAULT NULL,
  `Email` varchar(255) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  `LoginTime` datetime DEFAULT NULL,
  `LoginIp` varchar(255) DEFAULT NULL,
  `RoleId` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `Manager` */

insert  into `Manager`(`Id`,`UserName`,`Password`,`Mobile`,`Email`,`Status`,`LoginTime`,`LoginIp`,`RoleId`) values ('h5oMfYYCAkO46adFX7NQmQ','test9999',NULL,'15019400555','sdfsdf@qq.com',0,'0001-01-01 00:00:00',NULL,'K_H9jzB3a0WGG9NM8bEkGg,MXf_J9dUNkiAonC_GST1OQ'),('Rue835Tu8E2Lps38vJJu2Q','1','12123','2','3',1,'0001-01-01 00:00:00',NULL,'K_H9jzB3a0WGG9NM8bEkGg,MXf_J9dUNkiAonC_GST1OQ');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
