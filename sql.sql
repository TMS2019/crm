USE [crm]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Answers]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answers](
	[Id] [int] NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[ScenarioAnswerCode] [nvarchar](10) NOT NULL,
	[CustomerCode] [int] NOT NULL,
	[ScenarioCode] [nvarchar](9) NULL,
	[ScenerioScriptMappingCode] [nvarchar](10) NULL,
	[IDAsk] [int] NULL,
	[IDAnswer] [int] NULL,
	[Handling] [nvarchar](200) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Answers] PRIMARY KEY CLUSTERED 
(
	[ScenarioAnswerCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Campaigns]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Campaigns](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](200) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Campaigns] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CustomerProfileRefs]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerProfileRefs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[RowStatus] [smallint] NULL,
	[Type] [nvarchar](30) NULL,
	[Value] [nvarchar](20) NULL,
	[Text] [nvarchar](200) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.CustomerProfileRefs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Dealers]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dealers](
	[Id] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[DealerCode] [nvarchar](10) NOT NULL,
	[MainDealerId] [int] NULL,
	[MainDealerCode] [nvarchar](4) NULL,
	[SAPDealerCode] [nvarchar](12) NULL,
	[CustomerCode] [nvarchar](20) NULL,
	[VendorCode] [nvarchar](20) NULL,
	[DealerStatusUOMID] [int] NULL,
	[DealerStatusCode] [nvarchar](20) NULL,
	[DealerGroupID] [int] NOT NULL,
	[DealerGroupCode] [nvarchar](20) NULL,
	[EstablishmentDate] [datetime] NULL,
	[DealerWorkingStatusUOMID] [int] NULL,
	[DealerWorkingStatus] [nvarchar](30) NULL,
	[Reference] [nvarchar](50) NULL,
	[DealerName] [nvarchar](70) NULL,
	[BrandCode] [nvarchar](70) NULL,
	[Address] [nvarchar](150) NULL,
	[KabupatenID] [int] NOT NULL,
	[KabupatenCode] [nvarchar](50) NULL,
	[KabupatenName] [nvarchar](50) NULL,
	[AreaID] [int] NOT NULL,
	[Telp1] [nvarchar](50) NULL,
	[Telp2] [nvarchar](50) NULL,
	[HP] [nvarchar](50) NULL,
	[Fax] [nvarchar](50) NULL,
	[IsDeleted] [bit] NOT NULL,
	[isActive] [tinyint] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_dbo.Dealers] PRIMARY KEY CLUSTERED 
(
	[DealerCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Dummies]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dummies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[DummyName] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Dummies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Employees]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[ID] [int] NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[CRMID] [int] IDENTITY(1,1) NOT NULL,
	[MainDealerID] [int] NULL,
	[MainDealerCode] [nvarchar](4) NULL,
	[DealerID] [int] NULL,
	[DealerCode] [nvarchar](10) NULL,
	[POSID] [int] NOT NULL,
	[POSCode] [nvarchar](10) NULL,
	[HSOID] [nvarchar](50) NULL,
	[HondaID] [nvarchar](50) NULL,
	[NPK] [nvarchar](50) NULL,
	[TeamLeaderID] [int] NOT NULL,
	[SalesmanStatusUOMID] [int] NOT NULL,
	[EmployeeStatus] [nvarchar](100) NULL,
	[Name] [nvarchar](70) NULL,
	[IsSalesActive] [int] NOT NULL,
	[PositionID] [int] NOT NULL,
	[EmployeePosition] [nvarchar](100) NULL,
	[JoinDate] [datetime] NOT NULL,
	[IsInactiveBySystem] [int] NOT NULL,
	[TimeStatus] [timestamp] NOT NULL,
	[RowStatus] [smallint] NOT NULL,
	[ETLBatchRunID] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Employees] PRIMARY KEY CLUSTERED 
(
	[CRMID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Kabupatens]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kabupatens](
	[Id] [int] NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[KabupatenCode] [nvarchar](128) NOT NULL,
	[KabupatenName] [nvarchar](max) NULL,
	[ProvinceID] [int] NOT NULL,
	[ProvinceCode] [nvarchar](128) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Kabupatens] PRIMARY KEY CLUSTERED 
(
	[KabupatenCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Kecamatans]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kecamatans](
	[Id] [int] NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[KecamatanCode] [nvarchar](128) NOT NULL,
	[KecamatanName] [nvarchar](max) NULL,
	[KabupatenId] [int] NOT NULL,
	[KabupatenCode] [nvarchar](128) NULL,
	[ProvinceID] [int] NOT NULL,
	[ProvinceCode] [nvarchar](128) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Kecamatans] PRIMARY KEY CLUSTERED 
(
	[KecamatanCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Kelurahans]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kelurahans](
	[Id] [int] NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[KelurahanCode] [nvarchar](128) NOT NULL,
	[KelurahanName] [nvarchar](max) NULL,
	[KecamatanId] [int] NOT NULL,
	[KecamatanCode] [nvarchar](128) NULL,
	[KabupatenId] [int] NOT NULL,
	[KabupatenCode] [nvarchar](128) NULL,
	[ProvinceID] [int] NOT NULL,
	[ProvinceCode] [nvarchar](128) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Kelurahans] PRIMARY KEY CLUSTERED 
(
	[KelurahanCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Leads]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Leads](
	[Id] [int] NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[CRMCustomerCode] [int] IDENTITY(1,1) NOT NULL,
	[TimeStatus] [timestamp] NOT NULL,
	[RowStatus] [int] NOT NULL,
	[SourceSystemCreatedTime] [datetime] NOT NULL,
	[SourceSystemCreatedBy] [nvarchar](50) NULL,
	[SourceSystemLastModifiedTime] [datetime] NOT NULL,
	[SourceSystemLastModifiedBy] [nvarchar](50) NULL,
	[IDCardNo] [nvarchar](50) NULL,
	[CustomerCode] [nvarchar](1) NULL,
	[BirthDate] [datetime] NOT NULL,
	[Address] [nvarchar](150) NULL,
	[Gender] [nvarchar](1) NULL,
	[Religion] [nvarchar](1) NULL,
	[Profesion] [nvarchar](2) NULL,
	[Spending] [nvarchar](2) NULL,
	[Education] [nvarchar](2) NULL,
	[Name] [nvarchar](50) NULL,
	[CellNo] [nvarchar](25) NULL,
	[isCallable] [nvarchar](1) NULL,
	[Email] [nvarchar](50) NULL,
	[LastDealerName] [nvarchar](10) NULL,
	[LastSalesNo] [nvarchar](10) NULL,
	[LastSalesName] [nvarchar](50) NULL,
	[CurrentDealer] [nvarchar](10) NULL,
	[CurrentSalesNo] [nvarchar](10) NULL,
	[CurrentSalesName] [nvarchar](50) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Leads] PRIMARY KEY CLUSTERED 
(
	[CRMCustomerCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LeadsTemporaries]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeadsTemporaries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[CustomerCode] [nvarchar](max) NULL,
	[BirthDate] [datetime] NULL,
	[Address] [nvarchar](150) NULL,
	[Gender] [nvarchar](1) NULL,
	[Religion] [nvarchar](1) NULL,
	[Profesion] [nvarchar](2) NULL,
	[Spending] [nvarchar](2) NULL,
	[Education] [nvarchar](2) NULL,
	[Name] [nvarchar](50) NULL,
	[CellNo] [nvarchar](25) NULL,
	[isCallable] [nvarchar](1) NULL,
	[Email] [nvarchar](50) NULL,
	[IsDeleted] [bit] NOT NULL,
	[IDCardNo] [nvarchar](50) NULL,
 CONSTRAINT [PK_dbo.LeadsTemporaries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LeadsUnitTransactions]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeadsUnitTransactions](
	[Id] [int] NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[EngineCode] [nvarchar](6) NOT NULL,
	[EngineNo] [nvarchar](8) NOT NULL,
	[TimeStatus] [timestamp] NOT NULL,
	[RowStatus] [smallint] NOT NULL,
	[SourceSystemCreatedTime] [datetime] NOT NULL,
	[SourceSystemCreatedBy] [nvarchar](50) NULL,
	[SourceSystemLastModifiedTime] [datetime] NOT NULL,
	[SourceSystemLastModifiedBy] [nvarchar](50) NULL,
	[CRMCustomerCode] [int] NOT NULL,
	[ItemNo] [int] NOT NULL,
	[SourceSystem] [nvarchar](50) NULL,
	[SourceData] [nvarchar](50) NULL,
	[BikeUsage] [nvarchar](1) NULL,
	[BikeUser] [nvarchar](1) NULL,
	[PaymentType] [nvarchar](1) NULL,
	[TglBeliDLR] [datetime] NOT NULL,
	[MainDealerCode] [nvarchar](4) NULL,
	[UnitTypeCode] [nvarchar](4) NULL,
	[UnitVariantCode] [nvarchar](4) NULL,
	[UnitMarketName] [nvarchar](50) NULL,
	[SalesPersonNo] [nvarchar](10) NULL,
	[MappingBPSvsCDDBID] [int] NOT NULL,
	[HondaID] [nvarchar](50) NULL,
	[DealerCode] [nvarchar](10) NULL,
	[DealerName] [nvarchar](70) NULL,
	[Address] [nvarchar](150) NULL,
	[Telp1] [nvarchar](50) NULL,
	[iSActive] [tinyint] NOT NULL,
	[MainDealerName] [nvarchar](50) NULL,
	[IsHSO] [tinyint] NOT NULL,
	[KelurahanCode] [nvarchar](50) NULL,
	[Kelurahan] [nvarchar](50) NULL,
	[Kecamatan] [nvarchar](50) NULL,
	[KabupatenCode] [nvarchar](50) NULL,
	[Kabupaten] [nvarchar](50) NULL,
	[ProvinceCode] [nvarchar](50) NULL,
	[Province] [nvarchar](50) NULL,
	[KecamatanCode] [nvarchar](50) NULL,
	[HSOID] [int] NOT NULL,
	[UnitTypeSegment] [nvarchar](20) NULL,
	[UnitTypeSeries] [nvarchar](50) NULL,
	[CompanyCodeCode] [nvarchar](4) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.LeadsUnitTransactions] PRIMARY KEY CLUSTERED 
(
	[EngineCode] ASC,
	[EngineNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LeadsUnitTransactionTemporaries]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeadsUnitTransactionTemporaries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[UnitMarketName] [nvarchar](50) NULL,
	[EngineCode] [nvarchar](6) NULL,
	[EngineNo] [nvarchar](8) NULL,
	[TglBeli] [datetime] NULL,
	[PaymentType] [nvarchar](1) NULL,
	[ServiceType] [nvarchar](max) NULL,
	[SourceData] [nvarchar](max) NULL,
	[SeviceDate] [datetime] NULL,
	[Kelurahan] [nvarchar](50) NULL,
	[Kecamatan] [nvarchar](50) NULL,
	[Kabupaten] [nvarchar](50) NULL,
	[Provinsi] [nvarchar](50) NULL,
	[LeadsTemporaryId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UnitTypeSegment] [nvarchar](20) NULL,
	[UnitTypeSeries] [nvarchar](50) NULL,
	[MainDealerCode] [nvarchar](4) NULL,
	[MainDealerName] [nvarchar](50) NULL,
	[DealerCode] [nvarchar](10) NULL,
	[DealerName] [nvarchar](70) NULL,
 CONSTRAINT [PK_dbo.LeadsUnitTransactionTemporaries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MainDealers]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MainDealers](
	[Id] [int] NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[MainDealerCode] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[MainDealerName] [nvarchar](50) NULL,
	[MainDealerInitial] [nvarchar](50) NULL,
	[MainDealerDescription] [nvarchar](50) NULL,
	[MainDealerDelimiter] [nvarchar](50) NULL,
	[DPAmount] [nvarchar](5) NULL,
	[DPDate] [real] NOT NULL DEFAULT ((0)),
	[DPUpdatedUserName] [datetime] NULL,
	[RegionCode] [nvarchar](10) NULL,
	[isHSO] [nvarchar](4) NULL,
	[SAPAHMJournalAccountCode] [tinyint] NULL,
	[SAPHSOCustomerCode] [nvarchar](50) NULL,
	[Address] [nvarchar](100) NULL,
	[KabupatenID] [nvarchar](100) NULL,
	[BankAccountName] [nvarchar](100) NULL,
	[BankName] [nvarchar](50) NULL,
	[BankAccountNumber] [nvarchar](50) NULL,
	[Kelurahan] [nvarchar](50) NULL,
	[SupervisorName] [nvarchar](50) NULL,
	[Sequence] [nvarchar](50) NULL,
	[TimeStatus] [nvarchar](50) NULL,
	[ETLBatchRunID] [timestamp] NOT NULL,
	[Ring1] [int] NOT NULL DEFAULT ((0)),
	[Ring2] [int] NOT NULL DEFAULT ((0)),
	[Ring3] [int] NOT NULL DEFAULT ((0)),
	[RingOthers] [int] NOT NULL DEFAULT ((0)),
	[KabupatenCode] [int] NULL,
	[Rowstatus] [nvarchar](50) NULL,
	[ItemNo] [smallint] NOT NULL DEFAULT ((0)),
	[Latitude] [int] NULL,
	[Longitude] [real] NULL,
	[kecamatanID] [real] NULL,
	[KecamatanCode] [int] NULL,
 CONSTRAINT [PK_dbo.MainDealers] PRIMARY KEY CLUSTERED 
(
	[MainDealerCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MasterStatus]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterStatus](
	[Id] [int] NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[MasterStatusID] [int] IDENTITY(1,1) NOT NULL,
	[Value] [tinyint] NOT NULL,
	[Description] [nvarchar](50) NULL,
	[IsActive] [tinyint] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[StatusGroup] [nvarchar](30) NULL,
	[Name] [nvarchar](30) NULL,
 CONSTRAINT [PK_dbo.MasterStatus] PRIMARY KEY CLUSTERED 
(
	[MasterStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProspectFollowUps]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProspectFollowUps](
	[Id] [int] NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[ProspectFollowupID] [int] NOT NULL,
	[ProspectID] [int] NOT NULL,
	[ProspectStatus] [tinyint] NOT NULL,
	[FollowupDate] [datetime] NULL,
	[NextFollowupDate] [datetime] NULL,
	[Note] [nvarchar](200) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.ProspectFollowUps] PRIMARY KEY CLUSTERED 
(
	[ProspectFollowupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Prospects]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prospects](
	[Id] [int] NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[ProspectID] [int] IDENTITY(1,1) NOT NULL,
	[CRMCustomerNum] [int] NOT NULL,
	[ScenarioCode] [nvarchar](9) NULL,
	[CurrentSalesNo] [nvarchar](10) NULL,
	[CurrentSalesName] [nvarchar](50) NULL,
	[Notes] [nvarchar](200) NULL,
	[SuspectID] [nvarchar](10) NULL,
	[IsActive] [tinyint] NOT NULL,
	[CompanyCodeCode] [nvarchar](4) NULL,
	[IsDeleted] [bit] NOT NULL,
	[LastDealerName] [nvarchar](70) NULL,
	[LastSalesNo] [nvarchar](10) NULL,
	[LastSalesName] [nvarchar](50) NULL,
	[CurrentDealerCode] [nvarchar](10) NULL,
	[IsExpired] [bit] NOT NULL DEFAULT ((0)),
	[ExpiredDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.Prospects] PRIMARY KEY CLUSTERED 
(
	[ProspectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProspectTemporaries]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProspectTemporaries](
	[ProspectID] [int] IDENTITY(1,1) NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[CRMCustomerNum] [int] NOT NULL,
	[ScenarioCode] [nvarchar](9) NULL,
	[CurrentDealer] [nvarchar](10) NULL,
	[CurrentSalesNo] [nvarchar](10) NULL,
	[CurrentSalesName] [nvarchar](50) NULL,
	[ProspectStatus] [tinyint] NOT NULL,
	[Notes] [nvarchar](200) NULL,
	[MappingProspectCode] [nvarchar](20) NULL,
	[IsActive] [tinyint] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.ProspectTemporaries] PRIMARY KEY CLUSTERED 
(
	[ProspectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Provinces]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Provinces](
	[Id] [int] NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[ProvinceCode] [nvarchar](128) NOT NULL,
	[ProvinceName] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Provinces] PRIMARY KEY CLUSTERED 
(
	[ProvinceCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ScenarioFilters]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScenarioFilters](
	[Id] [int] NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[FillerCode] [nvarchar](20) NOT NULL,
	[TargetCustumerName] [nvarchar](50) NULL,
	[OdataQueryScript] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.ScenarioFilters] PRIMARY KEY CLUSTERED 
(
	[FillerCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ScenarioHistories]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScenarioHistories](
	[Id] [int] NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[MappingHistoryCode] [nvarchar](20) NOT NULL,
	[StatusCode] [tinyint] NULL,
	[RejectedBy] [int] NULL,
	[RejectReason] [nvarchar](200) NULL,
	[ApprovedBy] [int] NULL,
	[Date] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[SubmitionEmployeCode] [int] NULL,
 CONSTRAINT [PK_dbo.ScenarioHistories] PRIMARY KEY CLUSTERED 
(
	[MappingHistoryCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ScenarioLeadMappings]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScenarioLeadMappings](
	[Id] [int] NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[EngineCode] [nvarchar](6) NULL,
	[EngineNo] [nvarchar](8) NULL,
	[LeadScenarioMappingCode] [nvarchar](9) NOT NULL,
	[ScenarioCode] [nvarchar](9) NULL,
	[RefCampaignCode] [nvarchar](9) NULL,
	[CRMCustomerNum] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.ScenarioLeadMappings] PRIMARY KEY CLUSTERED 
(
	[LeadScenarioMappingCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Scenarios]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Scenarios](
	[Id] [int] NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[ScenarioCode] [nvarchar](9) NOT NULL,
	[ScenarioName] [nvarchar](200) NULL,
	[RefCampaignCode] [int] NULL,
	[isDefault] [tinyint] NOT NULL,
	[ScenarioDescription] [nvarchar](200) NULL,
	[Note] [nvarchar](200) NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[ResourceType] [tinyint] NOT NULL,
	[DestinationType] [tinyint] NOT NULL,
	[isSMS] [tinyint] NOT NULL,
	[isCall] [tinyint] NOT NULL,
	[StartDistributionSMSDate] [datetime] NOT NULL,
	[EndDistributionSMSDate] [datetime] NOT NULL,
	[DealerCode] [nvarchar](10) NULL,
	[SubmitionEmployeCode] [int] NULL,
	[MappingFillerCode] [nvarchar](20) NULL,
	[MappingHistoryCode] [nvarchar](20) NULL,
	[statusCode] [int] NULL,
	[CompanyCodeCode] [nvarchar](4) NULL,
	[IsDeleted] [bit] NOT NULL,
	[SmsContent] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Scenarios] PRIMARY KEY CLUSTERED 
(
	[ScenarioCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ScenarioScriptMappings]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScenarioScriptMappings](
	[Id] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[ScenarioScriptMappingCode] [nvarchar](10) NOT NULL,
	[Squence] [int] NOT NULL,
	[ScriptCode] [int] NOT NULL,
	[ScenarioCode] [nvarchar](9) NULL,
	[TransactionID] [tinyint] NULL,
	[TransactionType] [tinyint] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.ScenarioScriptMappings] PRIMARY KEY CLUSTERED 
(
	[ScenarioScriptMappingCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ScenarioSettings]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScenarioSettings](
	[Id] [int] NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[ScenarioSettingCode] [nvarchar](128) NOT NULL,
	[ScenarioCode] [nvarchar](9) NULL,
	[DealerCode] [nvarchar](10) NULL,
	[SmsCode] [nvarchar](128) NULL,
	[isAutomatic] [tinyint] NOT NULL,
	[isActive] [tinyint] NOT NULL,
	[MaxSms] [int] NOT NULL,
	[DataSortByDirection] [nvarchar](max) NULL,
	[StartDistributionSmsDate] [datetime] NOT NULL,
	[EndDistributionSmsDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.ScenarioSettings] PRIMARY KEY CLUSTERED 
(
	[ScenarioSettingCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Scripts]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Scripts](
	[Id] [int] NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[Scriptcode] [int] IDENTITY(1,1) NOT NULL,
	[Text] [nvarchar](200) NULL,
	[RefCode] [int] NOT NULL,
	[ScriptType] [tinyint] NOT NULL,
	[TypeQuestion] [tinyint] NOT NULL,
	[NextQuestion] [int] NOT NULL,
	[ScenarioCode] [nvarchar](9) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Scripts] PRIMARY KEY CLUSTERED 
(
	[Scriptcode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sms]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sms](
	[Id] [int] NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[SmsCode] [nvarchar](128) NOT NULL,
	[ScenarioCode] [nvarchar](9) NULL,
	[SmsContent] [nvarchar](max) NULL,
	[isDefault] [tinyint] NOT NULL,
	[DealerCode] [nvarchar](max) NULL,
	[MainDealerCode] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Sms] PRIMARY KEY CLUSTERED 
(
	[SmsCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SMSFollowups]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SMSFollowups](
	[Id] [int] NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[SMSFollowupID] [nvarchar](10) NOT NULL,
	[CRMCustomerNum] [int] NOT NULL,
	[ScenarioCode] [nvarchar](9) NULL,
	[CellNo] [nvarchar](50) NULL,
	[SMSContent] [nvarchar](10) NULL,
	[Status] [tinyint] NOT NULL,
	[Senddate] [datetime] NULL,
	[Count] [int] NOT NULL,
	[CompanyCodeCode] [nvarchar](4) NULL,
	[IsDeleted] [bit] NOT NULL,
	[isSync] [tinyint] NOT NULL,
	[Token] [nvarchar](max) NULL,
	[isUpdate] [tinyint] NOT NULL,
 CONSTRAINT [PK_dbo.SMSFollowups] PRIMARY KEY CLUSTERED 
(
	[SMSFollowupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SuspectFollowUps]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuspectFollowUps](
	[Id] [int] NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[SuspectFollowupID] [nvarchar](10) NOT NULL,
	[SuspectID] [nvarchar](10) NULL,
	[EmployeeID] [nvarchar](50) NULL,
	[CallStatus] [int] NOT NULL,
	[FollowupDate] [datetime] NOT NULL,
	[NextFollowupDate] [datetime] NOT NULL,
	[Note] [nvarchar](200) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.SuspectFollowUps] PRIMARY KEY CLUSTERED 
(
	[SuspectFollowupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Suspects]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suspects](
	[Id] [int] NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[SuspectID] [nvarchar](10) NOT NULL,
	[CRMCustomerNum] [int] NOT NULL,
	[ScenarioCode] [nvarchar](9) NULL,
	[LastPurchaseUnit] [nvarchar](50) NULL,
	[LastDealerName] [nvarchar](10) NULL,
	[LastSalesNo] [nvarchar](10) NULL,
	[LastSalesName] [nvarchar](50) NULL,
	[CurrentDealer] [nvarchar](10) NULL,
	[CurrentSalesNo] [nvarchar](10) NULL,
	[CurrentSalesName] [nvarchar](50) NULL,
	[SuspectStatus] [int] NOT NULL,
	[CompanyCodeCode] [nvarchar](4) NULL,
	[IsInactive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[LastReactivate] [datetime] NULL,
	[IsExpired] [bit] NOT NULL DEFAULT ((0)),
	[ExpiredDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.Suspects] PRIMARY KEY CLUSTERED 
(
	[SuspectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SuspectTemporaries]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuspectTemporaries](
	[SuspectID] [int] IDENTITY(1,1) NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[CRMCustomerNum] [int] NOT NULL,
	[ScenarioCode] [nvarchar](9) NULL,
	[LastPurchaseUnit] [nvarchar](50) NULL,
	[LastDealerName] [nvarchar](10) NULL,
	[LastSalesNo] [nvarchar](10) NULL,
	[LastSalesName] [nvarchar](50) NULL,
	[CurrentDealer] [nvarchar](10) NULL,
	[CurrentSalesNo] [nvarchar](10) NULL,
	[CurrentSalesName] [nvarchar](50) NULL,
	[SuspectStatus] [int] NOT NULL,
	[CompanyCodeCode] [nvarchar](4) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.SuspectTemporaries] PRIMARY KEY CLUSTERED 
(
	[SuspectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UnitPriceSettings]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UnitPriceSettings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[Merk] [nvarchar](max) NULL,
	[Varian] [nvarchar](max) NULL,
	[StartPrice] [nvarchar](max) NULL,
	[EndPrice] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.UnitPriceSettings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UnityTypeMarkets]    Script Date: 2/8/2018 7:51:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UnityTypeMarkets](
	[Id] [int] NOT NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[LastModifiedTime] [datetime] NULL,
	[LastModifiedBy] [nvarchar](50) NULL,
	[UnitTypeCode] [nvarchar](50) NOT NULL,
	[UnitMarketNameCode] [nvarchar](50) NULL,
	[UnitMarketName] [nvarchar](50) NULL,
	[UnitTypeSegmentID] [int] NOT NULL,
	[UnitTypeSegment] [nvarchar](20) NULL,
	[UnitTypeSeries] [nvarchar](50) NULL,
	[TimeStatus] [timestamp] NOT NULL,
	[Rowstatus] [smallint] NOT NULL,
	[ETLBatchRunID] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.UnityTypeMarkets] PRIMARY KEY CLUSTERED 
(
	[UnitTypeCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[SMSFollowups] ADD  DEFAULT ((0)) FOR [isSync]
GO
ALTER TABLE [dbo].[SMSFollowups] ADD  DEFAULT ((0)) FOR [isUpdate]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Kabupatens]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Kabupatens_dbo.Provinces_ProvinceCode] FOREIGN KEY([ProvinceCode])
REFERENCES [dbo].[Provinces] ([ProvinceCode])
GO
ALTER TABLE [dbo].[Kabupatens] CHECK CONSTRAINT [FK_dbo.Kabupatens_dbo.Provinces_ProvinceCode]
GO
ALTER TABLE [dbo].[Kecamatans]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Kecamatans_dbo.Kabupatens_KabupatenCode] FOREIGN KEY([KabupatenCode])
REFERENCES [dbo].[Kabupatens] ([KabupatenCode])
GO
ALTER TABLE [dbo].[Kecamatans] CHECK CONSTRAINT [FK_dbo.Kecamatans_dbo.Kabupatens_KabupatenCode]
GO
ALTER TABLE [dbo].[Kecamatans]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Kecamatans_dbo.Provinces_ProvinceCode] FOREIGN KEY([ProvinceCode])
REFERENCES [dbo].[Provinces] ([ProvinceCode])
GO
ALTER TABLE [dbo].[Kecamatans] CHECK CONSTRAINT [FK_dbo.Kecamatans_dbo.Provinces_ProvinceCode]
GO
ALTER TABLE [dbo].[Kelurahans]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Kelurahans_dbo.Kabupatens_KabupatenCode] FOREIGN KEY([KabupatenCode])
REFERENCES [dbo].[Kabupatens] ([KabupatenCode])
GO
ALTER TABLE [dbo].[Kelurahans] CHECK CONSTRAINT [FK_dbo.Kelurahans_dbo.Kabupatens_KabupatenCode]
GO
ALTER TABLE [dbo].[Kelurahans]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Kelurahans_dbo.Kecamatans_KecamatanCode] FOREIGN KEY([KecamatanCode])
REFERENCES [dbo].[Kecamatans] ([KecamatanCode])
GO
ALTER TABLE [dbo].[Kelurahans] CHECK CONSTRAINT [FK_dbo.Kelurahans_dbo.Kecamatans_KecamatanCode]
GO
ALTER TABLE [dbo].[Kelurahans]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Kelurahans_dbo.Provinces_ProvinceCode] FOREIGN KEY([ProvinceCode])
REFERENCES [dbo].[Provinces] ([ProvinceCode])
GO
ALTER TABLE [dbo].[Kelurahans] CHECK CONSTRAINT [FK_dbo.Kelurahans_dbo.Provinces_ProvinceCode]
GO
ALTER TABLE [dbo].[LeadsUnitTransactions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.LeadsUnitTransactions_dbo.Leads_CRMCustomerCode] FOREIGN KEY([CRMCustomerCode])
REFERENCES [dbo].[Leads] ([CRMCustomerCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LeadsUnitTransactions] CHECK CONSTRAINT [FK_dbo.LeadsUnitTransactions_dbo.Leads_CRMCustomerCode]
GO
ALTER TABLE [dbo].[LeadsUnitTransactionTemporaries]  WITH CHECK ADD  CONSTRAINT [FK_dbo.LeadsUnitTransactionTemporaries_dbo.LeadsTemporaries_LeadsTemporaryId] FOREIGN KEY([LeadsTemporaryId])
REFERENCES [dbo].[LeadsTemporaries] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LeadsUnitTransactionTemporaries] CHECK CONSTRAINT [FK_dbo.LeadsUnitTransactionTemporaries_dbo.LeadsTemporaries_LeadsTemporaryId]
GO
ALTER TABLE [dbo].[ProspectFollowUps]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProspectFollowUps_dbo.Prospects_ProspectID] FOREIGN KEY([ProspectID])
REFERENCES [dbo].[Prospects] ([ProspectID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProspectFollowUps] CHECK CONSTRAINT [FK_dbo.ProspectFollowUps_dbo.Prospects_ProspectID]
GO
ALTER TABLE [dbo].[Prospects]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Prospects_dbo.Dealers_CurrentDealerCode] FOREIGN KEY([CurrentDealerCode])
REFERENCES [dbo].[Dealers] ([DealerCode])
GO
ALTER TABLE [dbo].[Prospects] CHECK CONSTRAINT [FK_dbo.Prospects_dbo.Dealers_CurrentDealerCode]
GO
ALTER TABLE [dbo].[Prospects]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Prospects_dbo.Leads_CRMCustomerNum] FOREIGN KEY([CRMCustomerNum])
REFERENCES [dbo].[Leads] ([CRMCustomerCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Prospects] CHECK CONSTRAINT [FK_dbo.Prospects_dbo.Leads_CRMCustomerNum]
GO
ALTER TABLE [dbo].[Prospects]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Prospects_dbo.Scenarios_ScenarioCode] FOREIGN KEY([ScenarioCode])
REFERENCES [dbo].[Scenarios] ([ScenarioCode])
GO
ALTER TABLE [dbo].[Prospects] CHECK CONSTRAINT [FK_dbo.Prospects_dbo.Scenarios_ScenarioCode]
GO
ALTER TABLE [dbo].[Prospects]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Prospects_dbo.Suspects_SuspectID] FOREIGN KEY([SuspectID])
REFERENCES [dbo].[Suspects] ([SuspectID])
GO
ALTER TABLE [dbo].[Prospects] CHECK CONSTRAINT [FK_dbo.Prospects_dbo.Suspects_SuspectID]
GO
ALTER TABLE [dbo].[ProspectTemporaries]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProspectTemporaries_dbo.Leads_CRMCustomerNum] FOREIGN KEY([CRMCustomerNum])
REFERENCES [dbo].[Leads] ([CRMCustomerCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProspectTemporaries] CHECK CONSTRAINT [FK_dbo.ProspectTemporaries_dbo.Leads_CRMCustomerNum]
GO
ALTER TABLE [dbo].[ScenarioHistories]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ScenarioHistories_dbo.Employees_ApprovedBy] FOREIGN KEY([ApprovedBy])
REFERENCES [dbo].[Employees] ([CRMID])
GO
ALTER TABLE [dbo].[ScenarioHistories] CHECK CONSTRAINT [FK_dbo.ScenarioHistories_dbo.Employees_ApprovedBy]
GO
ALTER TABLE [dbo].[ScenarioHistories]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ScenarioHistories_dbo.Employees_RejectedBy] FOREIGN KEY([RejectedBy])
REFERENCES [dbo].[Employees] ([CRMID])
GO
ALTER TABLE [dbo].[ScenarioHistories] CHECK CONSTRAINT [FK_dbo.ScenarioHistories_dbo.Employees_RejectedBy]
GO
ALTER TABLE [dbo].[ScenarioLeadMappings]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ScenarioLeadMappings_dbo.Leads_CRMCustomerNum] FOREIGN KEY([CRMCustomerNum])
REFERENCES [dbo].[Leads] ([CRMCustomerCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ScenarioLeadMappings] CHECK CONSTRAINT [FK_dbo.ScenarioLeadMappings_dbo.Leads_CRMCustomerNum]
GO
ALTER TABLE [dbo].[ScenarioLeadMappings]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ScenarioLeadMappings_dbo.LeadsUnitTransactions_EngineCode_EngineNo] FOREIGN KEY([EngineCode], [EngineNo])
REFERENCES [dbo].[LeadsUnitTransactions] ([EngineCode], [EngineNo])
GO
ALTER TABLE [dbo].[ScenarioLeadMappings] CHECK CONSTRAINT [FK_dbo.ScenarioLeadMappings_dbo.LeadsUnitTransactions_EngineCode_EngineNo]
GO
ALTER TABLE [dbo].[ScenarioLeadMappings]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ScenarioLeadMappings_dbo.Scenarios_ScenarioCode] FOREIGN KEY([ScenarioCode])
REFERENCES [dbo].[Scenarios] ([ScenarioCode])
GO
ALTER TABLE [dbo].[ScenarioLeadMappings] CHECK CONSTRAINT [FK_dbo.ScenarioLeadMappings_dbo.Scenarios_ScenarioCode]
GO
ALTER TABLE [dbo].[Scenarios]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Scenarios_dbo.Campaigns_RefCampaignCode] FOREIGN KEY([RefCampaignCode])
REFERENCES [dbo].[Campaigns] ([Id])
GO
ALTER TABLE [dbo].[Scenarios] CHECK CONSTRAINT [FK_dbo.Scenarios_dbo.Campaigns_RefCampaignCode]
GO
ALTER TABLE [dbo].[Scenarios]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Scenarios_dbo.Dealers_DealerCode] FOREIGN KEY([DealerCode])
REFERENCES [dbo].[Dealers] ([DealerCode])
GO
ALTER TABLE [dbo].[Scenarios] CHECK CONSTRAINT [FK_dbo.Scenarios_dbo.Dealers_DealerCode]
GO
ALTER TABLE [dbo].[Scenarios]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Scenarios_dbo.Employees_SubmitionEmployeCode] FOREIGN KEY([SubmitionEmployeCode])
REFERENCES [dbo].[Employees] ([CRMID])
GO
ALTER TABLE [dbo].[Scenarios] CHECK CONSTRAINT [FK_dbo.Scenarios_dbo.Employees_SubmitionEmployeCode]
GO
ALTER TABLE [dbo].[Scenarios]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Scenarios_dbo.MasterStatus_StatusCode] FOREIGN KEY([statusCode])
REFERENCES [dbo].[MasterStatus] ([MasterStatusID])
GO
ALTER TABLE [dbo].[Scenarios] CHECK CONSTRAINT [FK_dbo.Scenarios_dbo.MasterStatus_StatusCode]
GO
ALTER TABLE [dbo].[Scenarios]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Scenarios_dbo.ScenarioFilters_MappingFillerCode] FOREIGN KEY([MappingFillerCode])
REFERENCES [dbo].[ScenarioFilters] ([FillerCode])
GO
ALTER TABLE [dbo].[Scenarios] CHECK CONSTRAINT [FK_dbo.Scenarios_dbo.ScenarioFilters_MappingFillerCode]
GO
ALTER TABLE [dbo].[Scenarios]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Scenarios_dbo.ScenarioHistories_MappingHistoryCode] FOREIGN KEY([MappingHistoryCode])
REFERENCES [dbo].[ScenarioHistories] ([MappingHistoryCode])
GO
ALTER TABLE [dbo].[Scenarios] CHECK CONSTRAINT [FK_dbo.Scenarios_dbo.ScenarioHistories_MappingHistoryCode]
GO
ALTER TABLE [dbo].[ScenarioScriptMappings]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ScenarioScriptMappings_dbo.Scenarios_ScenarioCode] FOREIGN KEY([ScenarioCode])
REFERENCES [dbo].[Scenarios] ([ScenarioCode])
GO
ALTER TABLE [dbo].[ScenarioScriptMappings] CHECK CONSTRAINT [FK_dbo.ScenarioScriptMappings_dbo.Scenarios_ScenarioCode]
GO
ALTER TABLE [dbo].[ScenarioScriptMappings]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ScenarioScriptMappings_dbo.Scripts_ScriptCode] FOREIGN KEY([ScriptCode])
REFERENCES [dbo].[Scripts] ([Scriptcode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ScenarioScriptMappings] CHECK CONSTRAINT [FK_dbo.ScenarioScriptMappings_dbo.Scripts_ScriptCode]
GO
ALTER TABLE [dbo].[ScenarioSettings]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ScenarioSettings_dbo.Dealers_DealerCode] FOREIGN KEY([DealerCode])
REFERENCES [dbo].[Dealers] ([DealerCode])
GO
ALTER TABLE [dbo].[ScenarioSettings] CHECK CONSTRAINT [FK_dbo.ScenarioSettings_dbo.Dealers_DealerCode]
GO
ALTER TABLE [dbo].[ScenarioSettings]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ScenarioSettings_dbo.Scenarios_ScenarioCode] FOREIGN KEY([ScenarioCode])
REFERENCES [dbo].[Scenarios] ([ScenarioCode])
GO
ALTER TABLE [dbo].[ScenarioSettings] CHECK CONSTRAINT [FK_dbo.ScenarioSettings_dbo.Scenarios_ScenarioCode]
GO
ALTER TABLE [dbo].[ScenarioSettings]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ScenarioSettings_dbo.Sms_SmsCode] FOREIGN KEY([SmsCode])
REFERENCES [dbo].[Sms] ([SmsCode])
GO
ALTER TABLE [dbo].[ScenarioSettings] CHECK CONSTRAINT [FK_dbo.ScenarioSettings_dbo.Sms_SmsCode]
GO
ALTER TABLE [dbo].[Scripts]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Scripts_dbo.Scenarios_ScenarioCode] FOREIGN KEY([ScenarioCode])
REFERENCES [dbo].[Scenarios] ([ScenarioCode])
GO
ALTER TABLE [dbo].[Scripts] CHECK CONSTRAINT [FK_dbo.Scripts_dbo.Scenarios_ScenarioCode]
GO
ALTER TABLE [dbo].[Sms]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Sms_dbo.Scenarios_ScenarioCode] FOREIGN KEY([ScenarioCode])
REFERENCES [dbo].[Scenarios] ([ScenarioCode])
GO
ALTER TABLE [dbo].[Sms] CHECK CONSTRAINT [FK_dbo.Sms_dbo.Scenarios_ScenarioCode]
GO
ALTER TABLE [dbo].[SMSFollowups]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SMSFollowups_dbo.Leads_CRMCustomerNum] FOREIGN KEY([CRMCustomerNum])
REFERENCES [dbo].[Leads] ([CRMCustomerCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SMSFollowups] CHECK CONSTRAINT [FK_dbo.SMSFollowups_dbo.Leads_CRMCustomerNum]
GO
ALTER TABLE [dbo].[SMSFollowups]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SMSFollowups_dbo.Scenarios_ScenarioCode] FOREIGN KEY([ScenarioCode])
REFERENCES [dbo].[Scenarios] ([ScenarioCode])
GO
ALTER TABLE [dbo].[SMSFollowups] CHECK CONSTRAINT [FK_dbo.SMSFollowups_dbo.Scenarios_ScenarioCode]
GO
ALTER TABLE [dbo].[SuspectFollowUps]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SuspectFollowUps_dbo.MasterStatus_CallStatus] FOREIGN KEY([CallStatus])
REFERENCES [dbo].[MasterStatus] ([MasterStatusID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SuspectFollowUps] CHECK CONSTRAINT [FK_dbo.SuspectFollowUps_dbo.MasterStatus_CallStatus]
GO
ALTER TABLE [dbo].[SuspectFollowUps]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SuspectFollowUps_dbo.Suspects_SuspectID] FOREIGN KEY([SuspectID])
REFERENCES [dbo].[Suspects] ([SuspectID])
GO
ALTER TABLE [dbo].[SuspectFollowUps] CHECK CONSTRAINT [FK_dbo.SuspectFollowUps_dbo.Suspects_SuspectID]
GO
ALTER TABLE [dbo].[Suspects]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Suspects_dbo.Leads_CRMCustomerNum] FOREIGN KEY([CRMCustomerNum])
REFERENCES [dbo].[Leads] ([CRMCustomerCode])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Suspects] CHECK CONSTRAINT [FK_dbo.Suspects_dbo.Leads_CRMCustomerNum]
GO
ALTER TABLE [dbo].[Suspects]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Suspects_dbo.MasterStatus_SuspectStatus] FOREIGN KEY([SuspectStatus])
REFERENCES [dbo].[MasterStatus] ([MasterStatusID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Suspects] CHECK CONSTRAINT [FK_dbo.Suspects_dbo.MasterStatus_SuspectStatus]
GO
ALTER TABLE [dbo].[Suspects]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Suspects_dbo.Scenarios_ScenarioCode] FOREIGN KEY([ScenarioCode])
REFERENCES [dbo].[Scenarios] ([ScenarioCode])
GO
ALTER TABLE [dbo].[Suspects] CHECK CONSTRAINT [FK_dbo.Suspects_dbo.Scenarios_ScenarioCode]
GO
ALTER TABLE [dbo].[SuspectTemporaries]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SuspectTemporaries_dbo.LeadsTemporaries_CRMCustomerNum] FOREIGN KEY([CRMCustomerNum])
REFERENCES [dbo].[LeadsTemporaries] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SuspectTemporaries] CHECK CONSTRAINT [FK_dbo.SuspectTemporaries_dbo.LeadsTemporaries_CRMCustomerNum]
GO
ALTER TABLE [dbo].[SuspectTemporaries]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SuspectTemporaries_dbo.Scenarios_ScenarioCode] FOREIGN KEY([ScenarioCode])
REFERENCES [dbo].[Scenarios] ([ScenarioCode])
GO
ALTER TABLE [dbo].[SuspectTemporaries] CHECK CONSTRAINT [FK_dbo.SuspectTemporaries_dbo.Scenarios_ScenarioCode]
GO
