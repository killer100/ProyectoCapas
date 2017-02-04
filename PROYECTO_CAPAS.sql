
CREATE DATABASE DB_TIENDA

USE DB_TIENDA

/*
*TABLA ARTICULO
*/
create table articulo (
	cod_art char(4)primary key,
	descrip varchar(20) null,
	prec_unic money null,
	stock numeric null
)

/*
*TABLA CLIENTE
*/
create table cliente(
	cod_clie char(4)primary key,
	mon_ape varchar(25) not null,
	telef char (9) null,
	dni char(8) not null,
	dir varchar(30) null
)

/*
*TABLA DETALLE
*/
create table detalle(
	num_fact numeric not null,
	cod_art char (4) not null,
	cant numeric null
)

/*
*TABLA FACTURA
*/
create table factura(
	num_fact numeric primary key,
	cod_clie char (4) not null,
	fech_vent datetime not null
)

/*DATOS TABLA ARTICULO*/
insert into articulo(cod_art,prec_unic,descrip,stock)
values('b001',15,'mouse',0)

insert into articulo(cod_art,prec_unic,descrip,stock)
values('b002',12,'teclado',3)

insert into articulo(cod_art,prec_unic,descrip,stock)
values('b003',10,'parlante',0)

insert into articulo(cod_art,prec_unic,descrip,stock)
values('b004',16,'audifono',2)

insert into articulo(cod_art,prec_unic,descrip,stock)
values('b005',20,'camara',3)

insert into articulo(cod_art,prec_unic,descrip,stock)
values('b006',40,'usb',10)

insert into articulo(cod_art,prec_unic,descrip,stock)
values('b007',1000,'microprocesador',11)

insert into articulo(cod_art,prec_unic,descrip,stock)
values('b008',150,'ram',8)

insert into articulo(cod_art,prec_unic,descrip,stock)
values('b009',2,'cds',100)

insert into articulo(cod_art,prec_unic,descrip,stock)
values('b010',3,'dvd',50)

insert into articulo(cod_art,prec_unic,descrip,stock)
values('b011',30,'sata',30)

insert into articulo(cod_art,prec_unic,descrip,stock)
values('b012',40,'flets',0)

insert into articulo(cod_art,prec_unic,descrip,stock)
values('b013',15,'culer',1)

insert into articulo(cod_art,prec_unic,descrip,stock)
values('b014',180,'bufer',18)

insert into articulo(cod_art,prec_unic,descrip,stock)
values('b015',250,'dscduro',20)

insert into articulo(cod_art,prec_unic,descrip,stock)
values('b016',160,'lectora',2)

insert into articulo(cod_art,prec_unic,descrip,stock)
values('b017',103,'web',6)

insert into articulo(cod_art,prec_unic,descrip,stock)
values('b018',23,'cargadores',12)

insert into articulo(cod_art,prec_unic,descrip,stock)
values('b019',350,'impresora',7)

insert into articulo(cod_art,prec_unic,descrip,stock)
values('b020',3500,'fotocopiadora',5)

insert into articulo(cod_art,prec_unic,descrip,stock)
values('b021',26,'conectores',25)

insert into articulo(cod_art,prec_unic,descrip,stock)
values('b022',25,'audio',50)

insert into articulo(cod_art,prec_unic,descrip,stock)
values('b023',12,'driver',100)

insert into articulo(cod_art,prec_unic,descrip,stock)
values('b024',200,'firmador',60)

insert into articulo(cod_art,prec_unic,descrip,stock)
values('b025',1535,'meyboart',70)

insert into articulo(cod_art,prec_unic,descrip,stock)
values('b026',156,'estabilizador',59)

insert into articulo(cod_art,prec_unic,descrip,stock)
values('b027',190,'fuente',0)

insert into articulo(cod_art,prec_unic,descrip,stock)
values('b028',80,'accespoint',29)

insert into articulo(cod_art,prec_unic,descrip,stock)
values('b029',250,'parrilla',6)

insert into articulo(cod_art,prec_unic,descrip,stock)
values('b030',123,'utp',0)


/*DATOS TABLA CLIENTE*/

insert into cliente(cod_clie,mon_ape,telef,dni)
values('a001','thomy torres','064589235','01567895')

insert into cliente(cod_clie,mon_ape,telef,dni)
values('a002','danmy torres','064259235','06567895')

insert into cliente(cod_clie,mon_ape,telef,dni)
values('a003','sonia belasco','484589235','58567895')

insert into cliente(cod_clie,mon_ape,telef,dni)
values('a004','nataly lozano','894589235','01569895')

insert into cliente(cod_clie,mon_ape,telef,dni)
values('a005','raul reyes','064595235','01525895')

/*DATOS TABLA FACTURA*/

insert into factura(num_fact,cod_clie,fech_vent)
values(001,'a001','03/20/2011')

insert into factura(num_fact,cod_clie,fech_vent)
values(002,'a002','04/20/2011')

insert into factura(num_fact,cod_clie,fech_vent)
values(003,'a003','05/20/2011')

insert into factura(num_fact,cod_clie,fech_vent)
values(004,'a004','06/20/2011')

insert into factura(num_fact,cod_clie,fech_vent)
values(005,'a005','07/20/2011')

insert into factura(num_fact,cod_clie,fech_vent)
values(006,'a006','08/20/2011')

insert into factura(num_fact,cod_clie,fech_vent)
values(007,'a007','09/20/2011')

insert into factura(num_fact,cod_clie,fech_vent)
values(008,'a008','10/20/2011')

insert into factura(num_fact,cod_clie,fech_vent)
values(009,'a009','03/20/2011')

insert into factura(num_fact,cod_clie,fech_vent)
values(010,'a010','11/20/2011')


/*DATOS TABLA CLIENTE*/

insert into detalle(num_fact,cod_art,cant)
values(001,'b001',0)

insert into detalle(num_fact,cod_art,cant)
values(002,'b017',3)

insert into detalle(num_fact,cod_art,cant)
values(003,'b003',0)

insert into detalle(num_fact,cod_art,cant)
values(004,'b004',1)

insert into detalle(num_fact,cod_art,cant)
values(005,'b005',3)

insert into detalle(num_fact,cod_art,cant)
values(006,'b006',10)

insert into detalle(num_fact,cod_art,cant)
values(007,'b007',11)

insert into detalle(num_fact,cod_art,cant)
values(008,'b008',8)

insert into detalle(num_fact,cod_art,cant)
values(009,'b009',100)

insert into detalle(num_fact,cod_art,cant)
values(010,'b030',29)

insert into detalle(num_fact,cod_art,cant)
values(002,'b028',0)

insert into detalle(num_fact,cod_art,cant)
values(004,'b022',50)

insert into detalle(num_fact,cod_art,cant)
values(006,'b018',12)

insert into detalle(num_fact,cod_art,cant)
values(010,'b019',7)

insert into detalle(num_fact,cod_art,cant)
values(001,'b016',2)





create view vw_facturas as
select  RIGHT('00000'+LTRIM(STR(a.num_fact)),5)+'-'+ltrim(STR(YEAR(a.fech_vent))) as num_fact_format, a.num_fact, c.cod_clie, c.mon_ape, a.fech_vent
from factura a
inner join cliente c on a.cod_clie = c.cod_clie

create view vw_detalle as 
select a.num_fact, a.cod_art, a.cant, b.descrip, b.prec_unic
from detalle a
inner join articulo b on a.cod_art = b.cod_art


create view vw_ventas as
select RIGHT('00000'+LTRIM(STR(a.num_fact)),5)+'-'+ltrim(STR(YEAR(a.fech_vent))) as num_fact, c.mon_ape, a.fech_vent, d.descrip as articulo, b.cant, b.cant*d.prec_unic as importe from factura a
inner join detalle b on a.num_fact = b.num_fact
inner join cliente c on a.cod_clie = c.cod_clie
inner join articulo d on b.cod_art = d.cod_art