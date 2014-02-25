-- phpMyAdmin SQL Dump
-- version 4.0.4.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Feb 25, 2014 at 10:29 AM
-- Server version: 5.5.32
-- PHP Version: 5.4.19

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `gisdb`
--
CREATE DATABASE IF NOT EXISTS `gisdb` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `gisdb`;

-- --------------------------------------------------------

--
-- Table structure for table `hsassets`
--

CREATE TABLE IF NOT EXISTS `hsassets` (
  `AssetsID` int(11) NOT NULL AUTO_INCREMENT,
  `AssetsName` varchar(200) NOT NULL,
  `Qty` int(11) NOT NULL,
  `Price` decimal(20,2) NOT NULL,
  `Note` varchar(250) NOT NULL,
  `DateIn` datetime NOT NULL,
  `UserIn` int(11) NOT NULL,
  `DateUp` datetime DEFAULT NULL,
  `UserUp` int(11) DEFAULT NULL,
  PRIMARY KEY (`AssetsID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `hscustomer`
--

CREATE TABLE IF NOT EXISTS `hscustomer` (
  `CustomerID` int(11) NOT NULL AUTO_INCREMENT,
  `CustomerName` varchar(100) NOT NULL,
  `Phone` varchar(50) NOT NULL,
  `Mobile` varchar(50) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `Address` varchar(250) NOT NULL,
  `DateOfBirth` date NOT NULL,
  `PlaceOfBirth` varchar(100) NOT NULL,
  `DateIn` datetime NOT NULL,
  `UserIn` int(11) NOT NULL,
  `DateUp` datetime DEFAULT NULL,
  `UserUp` int(11) DEFAULT NULL,
  PRIMARY KEY (`CustomerID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=7 ;

--
-- Dumping data for table `hscustomer`
--

INSERT INTO `hscustomer` (`CustomerID`, `CustomerName`, `Phone`, `Mobile`, `Email`, `Address`, `DateOfBirth`, `PlaceOfBirth`, `DateIn`, `UserIn`, `DateUp`, `UserUp`) VALUES
(5, 'Kevin Garnett', '0214505050', '0815707070', 'kg@email.com', 'Jl. Brooklyn Nets', '2014-02-25', 'NY', '2014-02-25 15:26:00', 7, '2014-02-25 15:26:47', 7),
(6, 'Yao Ming', '0214505050', '0815707070', 'kg@email.com', 'Jl. Brooklyn Nets', '2014-02-05', 'NY', '2014-02-25 15:26:00', 7, '2014-02-25 15:27:05', 7);

-- --------------------------------------------------------

--
-- Table structure for table `hspartner`
--

CREATE TABLE IF NOT EXISTS `hspartner` (
  `PartnerID` int(11) NOT NULL AUTO_INCREMENT,
  `Role` varchar(50) NOT NULL,
  `PartnerName` varchar(100) NOT NULL,
  `Phone` varchar(50) NOT NULL,
  `Mobile` varchar(50) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `Address` varchar(250) NOT NULL,
  `DateOfBirth` date NOT NULL,
  `PlaceOfBirth` varchar(100) NOT NULL,
  `DateIn` datetime NOT NULL,
  `UserIn` int(11) NOT NULL,
  `DateUp` datetime DEFAULT NULL,
  `UserUp` int(11) DEFAULT NULL,
  PRIMARY KEY (`PartnerID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `hsproduct`
--

CREATE TABLE IF NOT EXISTS `hsproduct` (
  `ProductID` int(11) NOT NULL AUTO_INCREMENT,
  `ProductName` varchar(200) NOT NULL,
  `Qty` int(11) NOT NULL,
  `Price` decimal(20,2) NOT NULL,
  `Note` varchar(250) NOT NULL,
  `DateIn` datetime NOT NULL,
  `UserIn` int(11) NOT NULL,
  `IsRetur` int(11) NOT NULL,
  `DateUp` datetime DEFAULT NULL,
  `UserUp` int(11) DEFAULT NULL,
  PRIMARY KEY (`ProductID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `msassets`
--

CREATE TABLE IF NOT EXISTS `msassets` (
  `AssetsID` int(11) NOT NULL AUTO_INCREMENT,
  `AssetsName` varchar(200) NOT NULL,
  `Qty` int(11) NOT NULL,
  `Price` decimal(20,2) NOT NULL,
  `Note` varchar(250) NOT NULL,
  `DateIn` datetime NOT NULL,
  `UserIn` int(11) NOT NULL,
  PRIMARY KEY (`AssetsID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `mscustomer`
--

CREATE TABLE IF NOT EXISTS `mscustomer` (
  `CustomerID` int(11) NOT NULL AUTO_INCREMENT,
  `CustomerName` varchar(100) NOT NULL,
  `Phone` varchar(50) NOT NULL,
  `Mobile` varchar(50) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `Address` varchar(250) NOT NULL,
  `DateOfBirth` date NOT NULL,
  `PlaceOfBirth` varchar(100) NOT NULL,
  `DateIn` datetime NOT NULL,
  `UserIn` int(11) NOT NULL,
  PRIMARY KEY (`CustomerID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=8 ;

--
-- Dumping data for table `mscustomer`
--

INSERT INTO `mscustomer` (`CustomerID`, `CustomerName`, `Phone`, `Mobile`, `Email`, `Address`, `DateOfBirth`, `PlaceOfBirth`, `DateIn`, `UserIn`) VALUES
(7, 'Dirk Nowitzky', '0214505050', '081590908080', 'mavs@email.com', 'Jl, Dallas Cowboy 41', '2014-02-25', 'TX', '2014-02-25 16:01:37', 7);

-- --------------------------------------------------------

--
-- Table structure for table `mspartner`
--

CREATE TABLE IF NOT EXISTS `mspartner` (
  `PartnerID` int(11) NOT NULL AUTO_INCREMENT,
  `Role` varchar(50) NOT NULL,
  `PartnerName` varchar(100) NOT NULL,
  `Phone` varchar(50) NOT NULL,
  `Mobile` varchar(50) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `Address` varchar(250) NOT NULL,
  `DateOfBirth` date NOT NULL,
  `PlaceOfBirth` varchar(100) NOT NULL,
  `DateIn` datetime NOT NULL,
  `UserIn` int(11) NOT NULL,
  PRIMARY KEY (`PartnerID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `msproduct`
--

CREATE TABLE IF NOT EXISTS `msproduct` (
  `ProductID` int(11) NOT NULL AUTO_INCREMENT,
  `ProductName` varchar(200) NOT NULL,
  `Qty` int(11) NOT NULL,
  `Price` decimal(20,2) NOT NULL,
  `Note` varchar(250) NOT NULL,
  `DateIn` datetime NOT NULL,
  `UserIn` int(11) NOT NULL,
  `IsRetur` int(11) NOT NULL,
  `HPP` decimal(20,2) NOT NULL,
  PRIMARY KEY (`ProductID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
