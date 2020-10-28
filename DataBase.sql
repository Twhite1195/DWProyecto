    IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'Proyecto')
  BEGIN
    CREATE DATABASE [Proyecto]


    END
    GO
       USE [Proyecto]
    GO


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CARRO') and o.name = 'FK_CARRO_REFERENCE_MARCA')
alter table CARRO
   drop constraint FK_CARRO_REFERENCE_MARCA
go

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
   where r.fkeyid = object_id('MODELO') and o.name = 'FK_MODELO_REFERENCE_MARCA')
alter table MODELO
   drop constraint FK_MODELO_REFERENCE_MARCA
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
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PROVEEDOR') and o.name = 'FK_PROVEEDOR_REFERENCE_SEDE')
alter table PROVEEDOR
   drop constraint FK_PROVEEDOR_REFERENCE_SEDE
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
           where  id = object_id('MARCA')
            and   type = 'U')
   drop table MARCA
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
           where  id = object_id('PROVEEDOR')
            and   type = 'U')
   drop table PROVEEDOR
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
   CAR_ID               int                not null,
   MAR_ID               int                null,
   MOD_ID               int                null,
   CAR_ESTADO           char(1)              null,
   CAR_PLACA           varchar             null,
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
   MOD_ID               int                  not null,
   MAR_ID               int        null,
   MOD_NOMBRE               varchar(Max)         null,
   constraint PK_MOD_ID primary key (MOD_ID)
)
go

/*==============================================================*/
/* Table: Marca                                             
==============================================================*/
create table MARCA (
   MAR_ID               int                 not null,
   MAR_NOMBRE           varchar(Max)         null,
   constraint PK_MAR_ID primary key (MAR_ID)
)
go

/*==============================================================*/
/* Table: Reservacion                                             
==============================================================*/
create table RESERVACION(
   RES_ID                 int                       not null,
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
   USU_ID               int                          not null,
   USU_CEDULA		        varchar(50)                    not null,
   USU_NOMBRE	        	varchar(50)        	           not null,
   USU_APELLIDO		      varchar(50)       	           not null,
   USU_TELEFONO		      int                         not null,
   USU_RESIDENCIA	      varchar(100)             	     not null,
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
   PUES_NOMBRE         varchar(100)          not null,
   PUES_ESTADO         char(1)          not null
   constraint PK_PUES_ID primary key (PUES_ID )
)
go

/*==============================================================*/
/* Table: Sede                                             
==============================================================*/
create table SEDE (
   SED_ID           int          not null,
   SED_NOMBRE               varchar(50)               null,
   SED_UBICACION        varchar(100)               null,
   constraint PK_Sede primary key (SED_ID)
)
go
/*==============================================================*/
/* Table: Proveedor                                             
==============================================================*/

create table PROVEEDOR (
   PROVE_ID         int           not null,
   SED_ID               int              null,
   PROVE_NOMBRE              varchar(100)                      not null,
   PROVE_FUNCION        varchar(100)               null,
   constraint PK_Proveedor primary key (PROVE_ID)
)
go
/*==============================================================*/
/* Table: Lote                                             
==============================================================*/
create table LOTE (
   LOT_ID           int          not null,
   SED_ID              int               null,
   LOTE_DISPONIBILIDAD             bit                    not null
   constraint PK_Cliente primary key (LOT_ID)
)
go

/*==============================================================*/
/* Table: Foreign Keys                                             
==============================================================*/

alter table CARRO
   add constraint FK_CARRO_REFERENCE_MARCA foreign key (MAR_ID)
      references MARCA (MAR_ID)
go

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

alter table MODELO
   add constraint FK_MODELO_REFERENCE_MARCA foreign key (MAR_ID)
      references MARCA (MAR_ID)
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

alter table PROVEEDOR
   add constraint FK_PROVEEDOR_REFERENCE_SEDE foreign key (SED_ID)
      references SEDE (SED_ID)
go