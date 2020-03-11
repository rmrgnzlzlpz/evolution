/*
 Navicat Premium Data Transfer

 Source Server         : MSSQL
 Source Server Type    : SQL Server
 Source Server Version : 14002027
 Source Host           : sql5052.site4now.net:1433
 Source Catalog        : DB_A561BE_evolution
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 14002027
 File Encoding         : 65001

 Date: 10/03/2020 23:49:58
*/


-- ----------------------------
-- Table structure for permissions
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[permissions]') AND type IN ('U'))
	DROP TABLE [dbo].[permissions]
GO

CREATE TABLE [dbo].[permissions] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [name] varchar(50) COLLATE Modern_Spanish_CI_AS  NOT NULL,
  [description] varchar(500) COLLATE Modern_Spanish_CI_AS  NULL,
  [state] bit  NOT NULL
)
GO

ALTER TABLE [dbo].[permissions] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of permissions
-- ----------------------------
SET IDENTITY_INSERT [dbo].[permissions] ON
GO

INSERT INTO [dbo].[permissions] ([id], [name], [description], [state]) VALUES (N'1', N'Iniciar Sesion', N'Iniciar Sesion', N'1')
GO

INSERT INTO [dbo].[permissions] ([id], [name], [description], [state]) VALUES (N'2', N'Crear Usuario', N'Crear Usuario', N'1')
GO

INSERT INTO [dbo].[permissions] ([id], [name], [description], [state]) VALUES (N'3', N'Crear Rol', N'Crear Rol', N'1')
GO

INSERT INTO [dbo].[permissions] ([id], [name], [description], [state]) VALUES (N'4', N'Actualizar Rol', N'Actualizar Rol', N'1')
GO

INSERT INTO [dbo].[permissions] ([id], [name], [description], [state]) VALUES (N'5', N'Ver Rol', N'Ver Rol', N'1')
GO

INSERT INTO [dbo].[permissions] ([id], [name], [description], [state]) VALUES (N'6', N'Eliminar Rol', N'Eliminar Rol', N'1')
GO

INSERT INTO [dbo].[permissions] ([id], [name], [description], [state]) VALUES (N'7', N'Generar Factura', N'Generar la factura de cuenta de cobro de una servidumbre especifica o de todas las servidumbres', N'1')
GO

INSERT INTO [dbo].[permissions] ([id], [name], [description], [state]) VALUES (N'8', N'Calculo Servidumbre', N'Ejecutar el cálculo por servidumbre', N'1')
GO

INSERT INTO [dbo].[permissions] ([id], [name], [description], [state]) VALUES (N'10', N'Editar Predio', N'Editar el Predio', N'1')
GO

INSERT INTO [dbo].[permissions] ([id], [name], [description], [state]) VALUES (N'11', N'Editar Servidumbre', N'Editar la Servidumbre', N'1')
GO

INSERT INTO [dbo].[permissions] ([id], [name], [description], [state]) VALUES (N'12', N'Navegar Opciones', N'Navegar Geograficamente todas las opciones', N'1')
GO

INSERT INTO [dbo].[permissions] ([id], [name], [description], [state]) VALUES (N'13', N'Ver Usuarios', N'Ver Usuarios', N'1')
GO

INSERT INTO [dbo].[permissions] ([id], [name], [description], [state]) VALUES (N'14', N'Editar Usuario', N'Editar Usuarios', N'1')
GO

SET IDENTITY_INSERT [dbo].[permissions] OFF
GO


-- ----------------------------
-- Table structure for roles
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[roles]') AND type IN ('U'))
	DROP TABLE [dbo].[roles]
GO

CREATE TABLE [dbo].[roles] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [name] varchar(30) COLLATE Modern_Spanish_CI_AS  NOT NULL,
  [description] varchar(500) COLLATE Modern_Spanish_CI_AS  NULL,
  [state] bit DEFAULT ((0)) NOT NULL
)
GO

ALTER TABLE [dbo].[roles] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of roles
-- ----------------------------
SET IDENTITY_INSERT [dbo].[roles] ON
GO

INSERT INTO [dbo].[roles] ([id], [name], [description], [state]) VALUES (N'1', N'Admin', N'Administrador', N'1')
GO

INSERT INTO [dbo].[roles] ([id], [name], [description], [state]) VALUES (N'2', N'Contador', N'Contador', N'1')
GO

INSERT INTO [dbo].[roles] ([id], [name], [description], [state]) VALUES (N'3', N'Tesorero', N'Tesrorero', N'1')
GO

INSERT INTO [dbo].[roles] ([id], [name], [description], [state]) VALUES (N'4', N'Gestor Predial', N'Gestor Predial', N'1')
GO

SET IDENTITY_INSERT [dbo].[roles] OFF
GO


-- ----------------------------
-- Table structure for roles_permissions
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[roles_permissions]') AND type IN ('U'))
	DROP TABLE [dbo].[roles_permissions]
GO

CREATE TABLE [dbo].[roles_permissions] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [role_id] int  NOT NULL,
  [permission_id] int  NOT NULL
)
GO

ALTER TABLE [dbo].[roles_permissions] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of roles_permissions
-- ----------------------------
SET IDENTITY_INSERT [dbo].[roles_permissions] ON
GO

INSERT INTO [dbo].[roles_permissions] ([id], [role_id], [permission_id]) VALUES (N'1', N'1', N'1')
GO

INSERT INTO [dbo].[roles_permissions] ([id], [role_id], [permission_id]) VALUES (N'2', N'1', N'2')
GO

INSERT INTO [dbo].[roles_permissions] ([id], [role_id], [permission_id]) VALUES (N'3', N'1', N'3')
GO

INSERT INTO [dbo].[roles_permissions] ([id], [role_id], [permission_id]) VALUES (N'4', N'1', N'4')
GO

INSERT INTO [dbo].[roles_permissions] ([id], [role_id], [permission_id]) VALUES (N'5', N'1', N'5')
GO

INSERT INTO [dbo].[roles_permissions] ([id], [role_id], [permission_id]) VALUES (N'6', N'1', N'6')
GO

INSERT INTO [dbo].[roles_permissions] ([id], [role_id], [permission_id]) VALUES (N'7', N'1', N'7')
GO

INSERT INTO [dbo].[roles_permissions] ([id], [role_id], [permission_id]) VALUES (N'8', N'1', N'8')
GO

INSERT INTO [dbo].[roles_permissions] ([id], [role_id], [permission_id]) VALUES (N'10', N'1', N'10')
GO

INSERT INTO [dbo].[roles_permissions] ([id], [role_id], [permission_id]) VALUES (N'11', N'1', N'11')
GO

INSERT INTO [dbo].[roles_permissions] ([id], [role_id], [permission_id]) VALUES (N'12', N'1', N'12')
GO

INSERT INTO [dbo].[roles_permissions] ([id], [role_id], [permission_id]) VALUES (N'14', N'1', N'14')
GO

INSERT INTO [dbo].[roles_permissions] ([id], [role_id], [permission_id]) VALUES (N'15', N'2', N'1')
GO

SET IDENTITY_INSERT [dbo].[roles_permissions] OFF
GO


-- ----------------------------
-- Table structure for sysdiagrams
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[sysdiagrams]') AND type IN ('U'))
	DROP TABLE [dbo].[sysdiagrams]
GO

CREATE TABLE [dbo].[sysdiagrams] (
  [name] sysname  NOT NULL,
  [principal_id] int  NOT NULL,
  [diagram_id] int  IDENTITY(1,1) NOT NULL,
  [version] int  NULL,
  [definition] varbinary(max)  NULL
)
GO

ALTER TABLE [dbo].[sysdiagrams] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for users
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[users]') AND type IN ('U'))
	DROP TABLE [dbo].[users]
GO

CREATE TABLE [dbo].[users] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [username] varchar(20) COLLATE Modern_Spanish_CI_AS  NOT NULL,
  [password] varchar(255) COLLATE Modern_Spanish_CI_AS  NOT NULL,
  [firstname] varchar(40) COLLATE Modern_Spanish_CI_AS  NOT NULL,
  [lastname] varchar(40) COLLATE Modern_Spanish_CI_AS  NOT NULL,
  [role_id] int  NOT NULL,
  [state] bit DEFAULT ((0)) NOT NULL
)
GO

ALTER TABLE [dbo].[users] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of users
-- ----------------------------
SET IDENTITY_INSERT [dbo].[users] ON
GO

INSERT INTO [dbo].[users] ([id], [username], [password], [firstname], [lastname], [role_id], [state]) VALUES (N'2', N'rmrgnzlz', N'AQAAAAEAACcQAAAAEH+GRvEsZL0glCHrM+McR0h+w/YeYGu/ZnKJYYSc6/sj0BQYtqYyqTMomZi7auv0QQ==', N'Ramiro', N'Gonzalez', N'1', N'1')
GO

INSERT INTO [dbo].[users] ([id], [username], [password], [firstname], [lastname], [role_id], [state]) VALUES (N'6', N'hsfa', N'AQAAAAEAACcQAAAAENcR9RLkZ0ImWVRYt46rdZkj36UUlyzNk3Rq1Gwgro81E1Yo9O8b4MqUWpBm7OPl8w==', N'Helmer', N'Fuentes', N'2', N'1')
GO

SET IDENTITY_INSERT [dbo].[users] OFF
GO


-- ----------------------------
-- Auto increment value for permissions
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[permissions]', RESEED, 14)
GO


-- ----------------------------
-- Primary Key structure for table permissions
-- ----------------------------
ALTER TABLE [dbo].[permissions] ADD CONSTRAINT [PK__permissi__3213E83FC2B75EA1] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for roles
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[roles]', RESEED, 4)
GO


-- ----------------------------
-- Uniques structure for table roles
-- ----------------------------
ALTER TABLE [dbo].[roles] ADD CONSTRAINT [UQ__roles__72E12F1BBB30AE9D] UNIQUE NONCLUSTERED ([name] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table roles
-- ----------------------------
ALTER TABLE [dbo].[roles] ADD CONSTRAINT [PK__roles__3213E83FE2B993DE] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for roles_permissions
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[roles_permissions]', RESEED, 15)
GO


-- ----------------------------
-- Uniques structure for table roles_permissions
-- ----------------------------
ALTER TABLE [dbo].[roles_permissions] ADD CONSTRAINT [UQ__roles_pe__C85A5462CFCB09B1] UNIQUE NONCLUSTERED ([role_id] ASC, [permission_id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table roles_permissions
-- ----------------------------
ALTER TABLE [dbo].[roles_permissions] ADD CONSTRAINT [PK__roles_pe__3213E83F20E57A28] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for sysdiagrams
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[sysdiagrams]', RESEED, 1)
GO


-- ----------------------------
-- Uniques structure for table sysdiagrams
-- ----------------------------
ALTER TABLE [dbo].[sysdiagrams] ADD CONSTRAINT [UK_principal_name] UNIQUE NONCLUSTERED ([principal_id] ASC, [name] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table sysdiagrams
-- ----------------------------
ALTER TABLE [dbo].[sysdiagrams] ADD CONSTRAINT [PK__sysdiagr__C2B05B61CEA43B89] PRIMARY KEY CLUSTERED ([diagram_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for users
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[users]', RESEED, 6)
GO


-- ----------------------------
-- Uniques structure for table users
-- ----------------------------
ALTER TABLE [dbo].[users] ADD CONSTRAINT [UQ__users__F3DBC572F72ABAC3] UNIQUE NONCLUSTERED ([username] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table users
-- ----------------------------
ALTER TABLE [dbo].[users] ADD CONSTRAINT [PK__users__3213E83FF9546FF8] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Foreign Keys structure for table roles_permissions
-- ----------------------------
ALTER TABLE [dbo].[roles_permissions] ADD CONSTRAINT [FK_roles_permissions_role_id] FOREIGN KEY ([role_id]) REFERENCES [dbo].[roles] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[roles_permissions] ADD CONSTRAINT [FK_roles_permissions_permission_id] FOREIGN KEY ([permission_id]) REFERENCES [dbo].[permissions] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table users
-- ----------------------------
ALTER TABLE [dbo].[users] ADD CONSTRAINT [FK_users_roles] FOREIGN KEY ([role_id]) REFERENCES [dbo].[roles] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

