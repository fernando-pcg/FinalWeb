
create table Socios(
	IdSocio int not null primary key identity,
	Nombre varchar(50) not null,
	Apellidos varchar(50) not null,
	Cedula bigint not null,
	Foto varchar(max) not null,
	Direccion varchar(60) not null,
	Telefono bigint not null,
	Sexo varchar(1) not null,
	Edad int not null,
	FechaNacimiento date not null,
	Afiliados int not null,
	Tipo_Membresia int not null default 1,
	LugarTrabajo varchar(60) not null,
	DireccionOficina varchar(30) not null,
	TelOficiona bigint not null,
	EstadoMembrecia int not null,
	FechaIngreso date default getdate(),
	FechaSalida date,
);


alter table Socios add constraint 
fk_Estados foreign key (EstadoMembrecia) references Estados(IdEstados)
alter table Socios add constraint 
fk_Afiliados foreign key (Afiliados) references Afiliados(IdAfiliado)

alter table Socios add constraint 
fk_MemberShip foreign key (Tipo_Membresia) references MemberShip(IdShup)


create table Usuarios(
	IdUser int primary key identity,
	Correo varchar(70) not null,
	Clave varchar(50) not null,
);

alter table Usuarios add constraint Uq_Correo unique (Correo)



select * from Usuarios 


truncate table Usuarios
create table MemberShip (
	IdShup int primary key identity,
	TipoMembrecia varchar(30),
);
insert into MemberShip (TipoMembrecia) values ('General')
insert into MemberShip (TipoMembrecia) values ('Premium')
insert into MemberShip (TipoMembrecia) values ('Premium++')

Create Table Estados(
	IdEstados int primary key identity,
	Estado varchar(15),
);
insert into Estados (Estado) values ('Activo')
insert into Estados (Estado) values ('Inactivo')

create table Afiliados(
	IdAfiliado int primary key identity,
	Afiliados varchar(30),
);
insert into Afiliados(Afiliados) values ('Pareja');
insert into Afiliados(Afiliados) values ('Hijos');
insert into Afiliados(Afiliados) values ('Otro');
