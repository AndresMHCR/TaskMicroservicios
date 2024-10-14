--==================================================================================================================
--BASE PERSONA CLIENTE

CREATE DATABASE Persona;
Go
Use Persona
Go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CLIENTE') and o.name = 'FK_CLIENTE_PERSONA_E_PERSONA')
alter table CLIENTE
   drop constraint FK_CLIENTE_PERSONA_E_PERSONA
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CLIENTE')
            and   name  = 'PERSONA_ES_CLIENTE_FK'
            and   indid > 0
            and   indid < 255)
   drop index CLIENTE.PERSONA_ES_CLIENTE_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CLIENTE')
            and   type = 'U')
   drop table CLIENTE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PERSONA')
            and   type = 'U')
   drop table PERSONA
go

/*==============================================================*/
/* Table: CLIENTE                                               */
/*==============================================================*/
create table CLIENTE (
   IDCLIENTE            int                  identity,
   IDPERSONA            int                  not null,
   CLIENTEID            varchar(16)          null,
   CONTRASENA           varchar(32)          null,
   ESTADOCLIENTE        bit                  null,
   constraint PK_CLIENTE primary key (IDCLIENTE)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('CLIENTE') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'schema', @CurrentUser, 'table', 'CLIENTE' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Modelo Cliente AH', 
   'schema', @CurrentUser, 'table', 'CLIENTE'
go

/*==============================================================*/
/* Index: PERSONA_ES_CLIENTE_FK                                 */
/*==============================================================*/




create nonclustered index PERSONA_ES_CLIENTE_FK on CLIENTE (IDPERSONA ASC)
go

/*==============================================================*/
/* Table: PERSONA                                               */
/*==============================================================*/
create table PERSONA (
   IDPERSONA            int                  identity,
   IDENTIFICACION       varchar(16)          null,
   NOMBRE               varchar(64)          null,
   GENERO               varchar(16)          null,
   EDAD                 int                  null,
   DIRECCION            varchar(64)          null,
   TELEFONO             varchar(16)          null,
   constraint PK_PERSONA primary key (IDPERSONA)
)
go

alter table CLIENTE
   add constraint FK_CLIENTE_PERSONA_E_PERSONA foreign key (IDPERSONA)
      references PERSONA (IDPERSONA)
go

--==================================================================================================================
--BASE CUENTA MOVIMIENTO

CREATE DATABASE Cuenta;
Go
Use Cuenta
Go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('MOVIMIENTO') and o.name = 'FK_MOVIMIEN_MOVIMIENT_CUENTA')
alter table MOVIMIENTO
   drop constraint FK_MOVIMIEN_MOVIMIENT_CUENTA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CUENTA')
            and   type = 'U')
   drop table CUENTA
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('MOVIMIENTO')
            and   name  = 'MOVIMIENTOS_CUENTA_FK'
            and   indid > 0
            and   indid < 255)
   drop index MOVIMIENTO.MOVIMIENTOS_CUENTA_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('MOVIMIENTO')
            and   type = 'U')
   drop table MOVIMIENTO
go

/*==============================================================*/
/* Table: CUENTA                                                */
/*==============================================================*/
create table CUENTA (
   IDCUENTA             int                  identity,
   NUMEROCUENTA         varchar(32)          null,
   TIPOCUENTA           varchar(16)          null,
   SALDOINICIAL         decimal(12,2)        null,
   ESTADOCUENTA         bit                  null,
   IDENTIFICACIONCLI    varchar(16)          null,
   constraint PK_CUENTA primary key (IDCUENTA)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('CUENTA') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'schema', @CurrentUser, 'table', 'CUENTA' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Entidad cuenta AH', 
   'schema', @CurrentUser, 'table', 'CUENTA'
go

/*==============================================================*/
/* Table: MOVIMIENTO                                            */
/*==============================================================*/
create table MOVIMIENTO (
   IDMOVIMIENTO         int                  identity,
   IDCUENTA             int                  not null,
   FECHA                datetime             null,
   TIPOMOVIMIENTO       varchar(16)          null,
   VALOR                decimal(12,2)        null,
   SALDO                decimal(12,2)        null,
   constraint PK_MOVIMIENTO primary key (IDMOVIMIENTO)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('MOVIMIENTO') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'schema', @CurrentUser, 'table', 'MOVIMIENTO' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Entidad Movimiento AH', 
   'schema', @CurrentUser, 'table', 'MOVIMIENTO'
go

/*==============================================================*/
/* Index: MOVIMIENTOS_CUENTA_FK                                 */
/*==============================================================*/




create nonclustered index MOVIMIENTOS_CUENTA_FK on MOVIMIENTO (IDCUENTA ASC)
go

alter table MOVIMIENTO
   add constraint FK_MOVIMIEN_MOVIMIENT_CUENTA foreign key (IDCUENTA)
      references CUENTA (IDCUENTA)
go

--==================================================================================================================
--BASE DE PERSONAS TEST DE INTEGRACION


CREATE DATABASE PersonaTest;
Go
Use PersonaTest
Go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CLIENTE') and o.name = 'FK_CLIENTE_PERSONA_E_PERSONA')
alter table CLIENTE
   drop constraint FK_CLIENTE_PERSONA_E_PERSONA
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CLIENTE')
            and   name  = 'PERSONA_ES_CLIENTE_FK'
            and   indid > 0
            and   indid < 255)
   drop index CLIENTE.PERSONA_ES_CLIENTE_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CLIENTE')
            and   type = 'U')
   drop table CLIENTE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PERSONA')
            and   type = 'U')
   drop table PERSONA
go

/*==============================================================*/
/* Table: CLIENTE                                               */
/*==============================================================*/
create table CLIENTE (
   IDCLIENTE            int                  identity,
   IDPERSONA            int                  not null,
   CLIENTEID            varchar(16)          null,
   CONTRASENA           varchar(32)          null,
   ESTADOCLIENTE        bit                  null,
   constraint PK_CLIENTE primary key (IDCLIENTE)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('CLIENTE') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'schema', @CurrentUser, 'table', 'CLIENTE' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Modelo Cliente AH', 
   'schema', @CurrentUser, 'table', 'CLIENTE'
go

/*==============================================================*/
/* Index: PERSONA_ES_CLIENTE_FK                                 */
/*==============================================================*/




create nonclustered index PERSONA_ES_CLIENTE_FK on CLIENTE (IDPERSONA ASC)
go

/*==============================================================*/
/* Table: PERSONA                                               */
/*==============================================================*/
create table PERSONA (
   IDPERSONA            int                  identity,
   IDENTIFICACION       varchar(16)          null,
   NOMBRE               varchar(64)          null,
   GENERO               varchar(16)          null,
   EDAD                 int                  null,
   DIRECCION            varchar(64)          null,
   TELEFONO             varchar(16)          null,
   constraint PK_PERSONA primary key (IDPERSONA)
)
go

alter table CLIENTE
   add constraint FK_CLIENTE_PERSONA_E_PERSONA foreign key (IDPERSONA)
      references PERSONA (IDPERSONA)
go


insert into PersonaTest..PERSONA (IDENTIFICACION, NOMBRE,GENERO, EDAD, DIRECCION, TELEFONO)
values('1723967699','Andres Huertas','Masculino',27,'Centro Historico',	'0962617760'),
	  ('1712217774','Marianela Montalvo','Femenino',35,'Amazonas y  NNUU','097548965'),
	  ('1750658799','Juan Osorio','Masculino',30,'13 junio y Equinoccial','098874587')
