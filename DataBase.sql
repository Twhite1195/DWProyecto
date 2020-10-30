/*
IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'Proyecto')
  BEGIN
    CREATE DATABASE [Proyecto]


    END
    GO
       USE [Proyecto]
    GO
*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CARRO') and o.name = 'FK_CARRO_REFERENCE_MODELO')
alter table CARRO
   drop constraint FK_CARRO_REFERENCE_MODELO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CARRO') and o.name = 'FK_CARRO_REFERENCE_SEDE')
alter table CARRO
   drop constraint FK_CARRO_REFERENCE_SEDE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CARRO') and o.name = 'FK_CARRO_REFERENCE_LOTE')
alter table CARRO
   drop constraint FK_CARRO_REFERENCE_LOTE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CARRO') and o.name = 'FK_CARRO_REFERENCE_RESERVACION')
alter table CARRO
   drop constraint FK_CARRO_REFERENCE_RESERVACION
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RESERVACION') and o.name = 'FK_RESERVACION_REFERENCE_LOTE')
alter table RESERVACION
   drop constraint FK_RESERVACION_REFERENCE_LOTE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RESERVACION') and o.name = 'FK_RESERVACION_REFERENCE_USUARIO')
alter table RESERVACION
   drop constraint FK_RESERVACION_REFERENCE_USUARIO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RESERVACION') and o.name = 'FK_RESERVACION_REFERENCE_CARRO')
alter table RESERVACION
   drop constraint FK_RESERVACION_REFERENCE_CARRO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('EMPLEADO') and o.name = 'FK_EMPLEADO_REFERENCE_SEDE')
alter table EMPLEADO
   drop constraint FK_EMPLEADO_REFERENCE_SEDE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('EMPLEADO') and o.name = 'FK_EMPLEADO_REFERENCE_PUESTO')
alter table EMPLEADO
   drop constraint FK_EMPLEADO_REFERENCE_PUESTO
go



if exists (select 1
            from  sysobjects
           where  id = object_id('CARRO')
            and   type = 'U')
   drop table CARRO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('MODELO')
            and   type = 'U')
   drop table MODELO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('RESERVACION')
            and   type = 'U')
   drop table RESERVACION
go

if exists (select 1
            from  sysobjects
           where  id = object_id('USUARIO')
            and   type = 'U')
   drop table USUARIO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('EMPLEADO')
            and   type = 'U')
   drop table EMPLEADO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PUESTO')
            and   type = 'U')
   drop table PUESTO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SEDE')
            and   type = 'U')
   drop table SEDE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LOTE')
            and   type = 'U')
   drop table LOTE
go

/*==============================================================*/
/* Table: Carro                                             
==============================================================*/
create table CARRO (
   CAR_ID               int Identity(1, 1)   not null,
   MOD_ID               int                null,
   CAR_ESTADO           char(1)              null,
   CAR_PLACA           varchar(Max)             null,
   SED_ID               int                null,
   LOT_ID               int                null,
   RES_ID               int                null,
   constraint PK_CAR_ID primary key (CAR_ID)
)
go

/*==============================================================*/
/* Table: Modelo                                             
==============================================================*/
create table MODELO (
   MOD_ID               int  Identity(1, 1)   not null,
   MOD_MARCA               varchar(max)        null,
   MOD_NOMBRE               varchar(Max)         null,
   constraint PK_MOD_ID primary key (MOD_ID)
)
go



/*==============================================================*/
/* Table: Reservacion                                             
==============================================================*/
create table RESERVACION(
   RES_ID                 int  Identity(1, 1)   not null,
   LOT_ID                 int                       not null,
   USU_ID                 int                       not null,
   CAR_ID                 int                       not null,
   RES_FECHA_INI          datetime                  not null,
   RES_FECHA_FIN          datetime                  not null,
   constraint PK_RES_ID primary key (RES_ID)

)
go


/*==============================================================*/
/* Table: Usuario
==============================================================*/
create table USUARIO (
   USU_ID               int   Identity(1, 1)        not null,
   USU_CEDULA		        varchar(50)                    not null,
   USU_PASSWORD            varchar(50)                    not null,     
   USU_NOMBRE	        	varchar(50)        	           not null,
   USU_APELLIDO		      varchar(50)       	           not null,
   USU_TELEFONO		      int                         not null,
   USU_ESTADO		        char(1)                        not null,
   USU_CORREO		        varchar(100)       	           not null,
   constraint PK_USU_ID primary key (USU_ID)
)
go

/*==============================================================*/
/* Table: EMPLEADO                                       
==============================================================*/
create table EMPLEADO (
   EMP_ID               int IDENTITY(1,1)    not null,
   SED_ID               int                  not null,
   PUES_ID              int                  not null,
   EMP_CEDULA           int                  not null,
   EMP_NOMBRE           varchar(100)         not null,
   EMP_APELLIDO         varchar(100)         not null,
   EMP_TELEFONO         varchar(100)         not null,
   EMP_RESIDENCIA       varchar(100)         not null,
   EMP_ESTADO           char(1)              not null
   constraint PK_EMP_ID primary key (EMP_ID)
)
go


/*==============================================================*/
/* Table: PUESTO                                       
==============================================================*/
create table PUESTO (
   PUES_ID             int IDENTITY(1,1)     not null,
   PUES_NOMBRE         varchar(100)          not null
   constraint PK_PUES_ID primary key (PUES_ID )
)
go

/*==============================================================*/
/* Table: Sede                                             
==============================================================*/
create table SEDE (
   SED_ID           int Identity(1, 1)    not null ,
   SED_NOMBRE               varchar(50)               null,
   SED_UBICACION        varchar(100)               null,
   constraint PK_Sede primary key (SED_ID)
)
go

/*==============================================================*/
/* Table: Lote                                             
==============================================================*/
create table LOTE (
   LOT_ID           int  Identity(1, 1)  not null, 
   SED_ID              int               null,
   LOTE_DISPONIBILIDAD             bit                    not null
   constraint PK_Cliente primary key (LOT_ID)
)
go

/*==============================================================*/
/* Table: Foreign Keys                                             
==============================================================*/

alter table CARRO
   add constraint FK_CARRO_REFERENCE_MODELO foreign key (MOD_ID)
      references MODELO (MOD_ID)
go

alter table CARRO
   add constraint FK_CARRO_REFERENCE_SEDE foreign key (SED_ID)
      references SEDE (SED_ID)
go

alter table CARRO
   add constraint FK_CARRO_REFERENCE_LOTE foreign key (LOT_ID)
      references LOTE (LOT_ID)
go

alter table CARRO
   add constraint FK_CARRO_REFERENCE_RESERVACION foreign key (RES_ID)
      references RESERVACION (RES_ID)
go


alter table RESERVACION
   add constraint FK_RESERVACION_REFERENCE_LOTE foreign key (LOT_ID)
      references LOTE (LOT_ID)
go

alter table RESERVACION
   add constraint FK_RESERVACION_REFERENCE_USUARIO foreign key (USU_ID)
      references USUARIO (USU_ID)
go

alter table RESERVACION
   add constraint FK_RESERVACION_REFERENCE_CARRO foreign key (CAR_ID)
      references CARRO (CAR_ID)
go

alter table EMPLEADO
   add constraint FK_EMPLEADO_REFERENCE_SEDE foreign key (SED_ID)
      references SEDE (SED_ID)
go

alter table EMPLEADO
   add constraint FK_EMPLEADO_REFERENCE_PUESTO foreign key (PUES_ID)
      references PUESTO (PUES_ID)
go



/*==============================================================*/
/* Table: INSERTS                                           
==============================================================*/


/* Table: Usuario*/
INSERT INTO USUARIO (USU_CEDULA,USU_PASSWORD,USU_NOMBRE,USU_APELLIDO,USU_TELEFONO,USU_ESTADO,USU_CORREO) VALUES (116260314,'12345','Thomas','White',15682256,'A','twhitev314@ulacit.ed.cr');
INSERT INTO USUARIO (USU_CEDULA,USU_PASSWORD,USU_NOMBRE,USU_APELLIDO,USU_TELEFONO,USU_ESTADO,USU_CORREO) VALUES (559959937,'12345','Ben','Dover',73974783,'A','BenDover@yahoo.com');
INSERT INTO USUARIO (USU_CEDULA,USU_PASSWORD,USU_NOMBRE,USU_APELLIDO,USU_TELEFONO,USU_ESTADO,USU_CORREO) VALUES (646511834,'12345','Dixie','Normus',45568228,'A','DixieNormus@gmail.com');

/* Table: PUESTO*/
INSERT INTO PUESTO (PUES_NOMBRE) VALUES('Administrador');
INSERT INTO PUESTO (PUES_NOMBRE) VALUES('Gerente');
INSERT INTO PUESTO (PUES_NOMBRE) VALUES('Recepcionista');
INSERT INTO PUESTO (PUES_NOMBRE) VALUES('Servicio Tecnico');

/* Table: SEDE*/

INSERT INTO SEDE (SED_NOMBRE,SED_UBICACION) VALUES('HQ', 'La Uruca,San José');
INSERT INTO SEDE (SED_NOMBRE,SED_UBICACION) VALUES('Paradise', 'Liberia,Guanacaste');
INSERT INTO SEDE (SED_NOMBRE,SED_UBICACION) VALUES('Ghetto', 'Lion XIII');

/* Table: EMPLEADO*/

INSERT INTO EMPLEADO (SED_ID,PUES_ID,EMP_CEDULA,EMP_NOMBRE,EMP_APELLIDO,EMP_TELEFONO,EMP_RESIDENCIA,EMP_ESTADO) VALUES (1,1,852843450,'Luigi','Riggatoni',86518861,'Calle Blancos','A');
INSERT INTO EMPLEADO (SED_ID,PUES_ID,EMP_CEDULA,EMP_NOMBRE,EMP_APELLIDO,EMP_TELEFONO,EMP_RESIDENCIA,EMP_ESTADO) VALUES (2,3,895683893,'Estela','Gartija',85162493,'San Pedro','A');
INSERT INTO EMPLEADO (SED_ID,PUES_ID,EMP_CEDULA,EMP_NOMBRE,EMP_APELLIDO,EMP_TELEFONO,EMP_RESIDENCIA,EMP_ESTADO) VALUES (3,4,273558344,'Elena','Nito',87621913,'Guadalupe','A');


/* Table: LOTE*/

INSERT INTO LOTE (SED_ID,LOTE_DISPONIBILIDAD) VALUES (1,1);
INSERT INTO LOTE (SED_ID,LOTE_DISPONIBILIDAD) VALUES (1,0);
INSERT INTO LOTE (SED_ID,LOTE_DISPONIBILIDAD) VALUES (1,0);
INSERT INTO LOTE (SED_ID,LOTE_DISPONIBILIDAD) VALUES (2,1);
INSERT INTO LOTE (SED_ID,LOTE_DISPONIBILIDAD) VALUES (2,0);
INSERT INTO LOTE (SED_ID,LOTE_DISPONIBILIDAD) VALUES (2,1);
INSERT INTO LOTE (SED_ID,LOTE_DISPONIBILIDAD) VALUES (3,1);
INSERT INTO LOTE (SED_ID,LOTE_DISPONIBILIDAD) VALUES (3,0);
INSERT INTO LOTE (SED_ID,LOTE_DISPONIBILIDAD) VALUES (3,0);


/* Table: MODELO*/
/*Toyota*/
INSERT INTO MODELO(Mod_MARCA,MOD_NOMBRE) VALUES ('Toyota','Yaris');
INSERT INTO MODELO(Mod_MARCA,MOD_NOMBRE) VALUES ('Toyota','Corolla');
INSERT INTO MODELO(Mod_MARCA,MOD_NOMBRE) VALUES ('Toyota','Supra');
INSERT INTO MODELO(Mod_MARCA,MOD_NOMBRE) VALUES ('Toyota','Hilux');
/*BMW*/
INSERT INTO MODELO(Mod_MARCA,MOD_NOMBRE) VALUES ('BMW','Serie 1');
INSERT INTO MODELO(Mod_MARCA,MOD_NOMBRE) VALUES ('BMW','Serie 2');
INSERT INTO MODELO(Mod_MARCA,MOD_NOMBRE) VALUES ('BMW','Serie 3');
INSERT INTO MODELO(Mod_MARCA,MOD_NOMBRE) VALUES ('BMW','Serie 4');
/*Hyundai*/
INSERT INTO MODELO(Mod_MARCA,MOD_NOMBRE) VALUES ('Hyundai','i10');
INSERT INTO MODELO(Mod_MARCA,MOD_NOMBRE) VALUES ('Hyundai','i20');
INSERT INTO MODELO(Mod_MARCA,MOD_NOMBRE) VALUES ('Hyundai','i30');
INSERT INTO MODELO(Mod_MARCA,MOD_NOMBRE) VALUES ('Hyundai','i40');
/*Honda*/
INSERT INTO MODELO(Mod_MARCA,MOD_NOMBRE) VALUES ('Honda','Civic');
INSERT INTO MODELO(Mod_MARCA,MOD_NOMBRE) VALUES ('Honda','CR-V');
INSERT INTO MODELO(Mod_MARCA,MOD_NOMBRE) VALUES ('Honda','Accord');
INSERT INTO MODELO(Mod_MARCA,MOD_NOMBRE) VALUES ('Honda','Oddyssey');
/*KIA*/
INSERT INTO MODELO(Mod_MARCA,MOD_NOMBRE) VALUES ('KIA','Sorento');
INSERT INTO MODELO(Mod_MARCA,MOD_NOMBRE) VALUES ('KIA','Rio');
INSERT INTO MODELO(Mod_MARCA,MOD_NOMBRE) VALUES ('KIA','Picanto');
INSERT INTO MODELO(Mod_MARCA,MOD_NOMBRE) VALUES ('KIA','Sportage');
/*Mazda*/
INSERT INTO MODELO(Mod_MARCA,MOD_NOMBRE) VALUES ('Mazda','3');
INSERT INTO MODELO(Mod_MARCA,MOD_NOMBRE) VALUES ('Mazda','CX-5');
INSERT INTO MODELO(Mod_MARCA,MOD_NOMBRE) VALUES ('Mazda','CX-30');
INSERT INTO MODELO(Mod_MARCA,MOD_NOMBRE) VALUES ('Mazda','MX-30');


/* Table: CARRO*/

INSERT INTO CARRO(MOD_ID,CAR_ESTADO,CAR_PLACA,SED_ID,LOT_ID) VALUES (1,'D','CRC-786',1,1);

INSERT INTO CARRO(MOD_ID,CAR_ESTADO,CAR_PLACA,SED_ID,LOT_ID) VALUES (7,'D','CRC-788',2,4);

/*Reservacion*/

INSERT INTO RESERVACION(LOT_ID,USU_ID,CAR_ID,RES_FECHA_INI,RES_FECHA_FIN) VALUES (1,1,2,'10/28/20 23:59:59.999','10/30/20 23:59:59.999');

/*Add reservacion to car*/

UPDATE CARRO
SET RES_ID = 1
WHERE CAR_ID = 1;









