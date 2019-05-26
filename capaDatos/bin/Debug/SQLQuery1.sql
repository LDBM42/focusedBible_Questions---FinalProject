create database focusedBible

use focusedBible

create table preguntas(
codPreg int primary key identity,
preg varchar(300) not null,
a varchar(200) not null,
b varchar(200) not null,
c varchar(200) not null,
d varchar(200) not null,
resp char(1) not null,
pasage varchar(50),
dificultad varchar(50))




/* ESTO SIRVE PARA PASAR EL VALOR DE UNA
 TABLA A OTRA PARA REINICIAR EL IDENTITY*/

/*Copiamos las filas a una tabla temporal*/
SELECT * INTO preguntasTemp FROM preguntas;

/*Eliminamos las filas de la tabla con TRUNCATE*/
TRUNCATE TABLE preguntas;

/*Pasamos las filas a la tabla nombrando las columnas menos la de IDENTITY*/
INSERT INTO preguntas (preg, a, b, c, d, resp, pasage, dificultad) SELECT preg, a, b, c, d, resp, pasage, dificultad FROM preguntasTemp;

/*Borramos la tabla temporal*/
DROP TABLE preguntasTemp;



insert into preguntas values('�Qui�n fue el primer rey de Israel?','David','Aar�n','Sa�l','Mois�s','c')
insert into preguntas values('�Con que rey se cas� Esther para convertirse en reina?','Dar�o','Asuero','Belsasar','Herodes','b')
insert into preguntas values('�Qui�n sirvi� como profeta en el reinado del rey Dar�o el persa?','Zacar�as','Hageo','Daniel','Jerem�as','c')
insert into preguntas values('�Qui�n deseaba la vi�a de Nabot tanto como para poder dormir?','Acab','Jeroboam','Jezabel','Manases','a')

insert into preguntas values('�En qu� d�a de la creaci�n Dios hizo los animales?','Segundo','Tercero','Cuarto','Quinto','d')
insert into preguntas values('�Qu� general del ej�rcito sirio fue leproso?','Naam�n','Ur�as','Acan','Jefte','a')
insert into preguntas values('�Cuantos disc�pulos estuvieron presentes en la transfiguraci�n?','Tres','Dos','Cuatro','Cinco','a')
insert into preguntas values('�C�mo se llamaba el Padre de Juan el bautista?','Jacobo','Zacar�as','Ezequ�as','Eli','b')
insert into preguntas values('�En que se origin� el pecado?','Ad�n','Ca�n','Eva','Lucifer','d')
insert into preguntas values('�Cu�l fue el nombre que Nabuconosor le puso a Azar�as?','Sadrac','Mesac','Abed-Nego','Beltsasar','c')
insert into preguntas values('�En la transfiguraci�n de Jes�s, este apareci� junto a Mois�s y','Eliseo','Enoc','El�as','Juan el Bautista','c')
insert into preguntas values('�Qui�n fue el primog�nito de Ad�n y Eva?','Cain','Abel','Set','Noe','a')
insert into preguntas values('�Qui�n fue el padre de Samuel?','Eliu','Esa�','Elcana','Eli','c')
insert into preguntas values('�Cuantas monedas le dieron a Judas por entregar a Jes�s?','40 ','30','20','50','b')
insert into preguntas values('�En qu� d�a de la creaci�n hizo Dios los �rboles y las plantas?','Primero','Segundo','Tercero','Cuarto','c')
insert into preguntas values('�Bienaventurados los pobres en esp�ritu porque','Ellos ver�n a Dios','De ellos es el reino','Ellos ser�n saciados','Ellos ser�n consolados','b')
insert into preguntas values('�Qu� pasaje del antiguo testamento le�a el et�ope cuando se encontr� con Felipe?','Daniel 2','�xodo 20','Isa�as 53','Salmo 23','c')
insert into preguntas values('�En qu� d�a de la creaci�n hizo Dios al hombre?','Cuarto','Quinto','Sexto','Tercero','c')
insert into preguntas values('�Qui�nes fueron los disc�pulos que le dieron a Jes�s �Conc�denos que en tu gloria nos sentemos uno a la derecha y el otro a tu izquierda�?','Andr�s y Juan','Juan y Pedro','Andr�s y Jacobo','Jacobo y juan','d')
insert into preguntas values('�En la par�bola de las diez v�rgenes. �Cu�ntas se durmieron? ','Cinco','Una','Diez','Dos','c')
insert into preguntas values('�En qu� d�a de la creaci�n separo Dios la luz de la oscuridad?','Primero','Segundo','Tercero','Cuarto','a')
insert into preguntas values('�C�mo eran los cabellos del personaje que mira el ap�stol Juan en Apocalipsis cap. 1?','Dorados como el oro','Como bronce Bru�ido','Blancos como la lana','Gris como la Espinela','c')
insert into preguntas values('�En apocalipsis que simbolizan las siete estrellas en la diestra de Jes�s?','Las 7 Iglesias','Los 7 �ngeles','Los 7 Juicios','Los 7 Esp�ritus','b')
insert into preguntas values('�En las bodas de Cana cuantas tinajas de piedra llenaron de agua?','4','5','6','7','c')
insert into preguntas values('�Con que dio de comer Jes�s a 5000 personas?','5 panes y 2 peces','7 panes y 3 peces','5 peces y 2 panes','4 panes y 3 peces','a')
insert into preguntas values('�Cu�l es el sexto mandamiento?','No tendr�s dioses ajenos','No Hurtaras','No Mataras','No Codiciaras','c')
insert into preguntas values('�Qu� libro relata la edificaci�n de los muros de Jerusal�n?','Jerem�as','Isa�as','Lamentaciones','Nehem�as','d')
insert into preguntas values('�En qu� libro se encuentra la profec�a del gran sufrimiento de Jes�s?','Abd�as','G�nesis','Jerem�as','Isa�as','d')
insert into preguntas values('�Qui�n dijo, ap�rtate de mi Se�or porque soy hombre pecador?','Pablo','Pedro','Andr�s','Mateo','b')
insert into preguntas values('�C�mo fueron llamados los jefes de Israel despu�s de la muerte de Josu�?','Reyes','Sacerdotes','Jueces','Levitas','c')

insert into preguntas values('�Por cuantos a�os estuvieron los jud�os en Egipto?','390','430','420','410','d')
insert into preguntas values('�Qui�n fue bendecido por tener en su casa el arca del Se�or?','David','Saul','Odeb-Edom','Obeb-Tubal','c')
insert into preguntas values('�De qui�n era esposa Herod�as?','Herodes','Felipe','Agripa','Pilato','c')
insert into preguntas values('�Durante el reinado de quien se hall� el libro de la ley que por mucho tiempo estaba perdido?','Salom�n','Ezequ�as','Isa�as','Jos�as','d')
insert into preguntas values('�A cu�ntos profetas de Baal degoll� El�as?','400','420','430','450','d')
insert into preguntas values('�A cu�ntos hombres alimento Eliseo con 20 panes de cebada?','100','200','300','5000','a')
insert into preguntas values('�C�mo se llam� el rey que vio una mano escribiendo en la pared?','Nabucodonosor','Daniel','Belsasar','Ciro','c')
insert into preguntas values('�Qui�n estuvo 3 d�as y tres noches en el vientre de un pez?','Sofon�as','Arquelao','Jon�s','Mateo','c')
insert into preguntas values('�Qu� hecho convirti� a David en h�roe para Israel?','Su victoria sobre Goliat','Sus poes�as','Su victoria sobre los babilonios','Su victoria sobre Jeric�','a')
insert into preguntas values('�Qui�n fue arrojado al foso de los leones y sali� intacto de ah�?','Juan Bautista','Daniel','Mois�s','Pablo','b')
insert into preguntas values('�A qui�n reclama Jes�s en Getseman�: "�Ni siquiera una hora pudiste mantener despierto?"','Juan','Pedro','Andr�s','Judas Tadeo','b')
insert into preguntas values('�Hijo de Abraham que estuvo a punto de ser sacrificado?','Esa�','Jon�s','Jos�','Isaac','d')
insert into preguntas values('Ap�stol que hab�a sido cobrador de impuestos para Roma.','Marcos','Mateo','Lucas','Juan','b')
insert into preguntas values('Junto con Jos� estaban en la c�rcel el _____ y el _____ del fara�n.','Amigo y el enemigo','Hermano y el primo','Copero y el Panadero','General y el consejero','c')
insert into preguntas values('Rey al que los magos visitaron en Judea para preguntar por el Mes�as.','David','Herodes','Cesar Augusto','Agripa','b')
insert into preguntas values('Primer hijo de Ad�n y Eva','Judas Macabeo','Abel','Daniel','Ca�n','d')
insert into preguntas values('�A la hija de qu� jefe de la sinagoga resucita Cristo?','Jairo','An�s','Zacar�as','El�','a')
insert into preguntas values('Qui�n sucede a David en el trono de Israel.','Miqueas','Salom�n','Roboam','Jeroboam','b')
insert into preguntas values('�Qu� profeta sinti� que la burra le hablaba?','Elias','Isaias','Zacar�as','Balaam','d')
insert into preguntas values('Rey de Babilonia que fabric� una estatua de s� y exigi� adorarla.','Nabucodonosor','Ciro','Rams�s','Sih�n','a')
insert into preguntas values('Ap�stol elegido para reemplazar a Judas.','Mat�as','Bernab�','Apolos','Silas','a')
insert into preguntas values('Nuera de Noem� que al morir el esposo la acompa�� hasta Bel�n. Un libro del A. T. lleva su nombre.','Ester','Judit','Rut','Rebeca','c')
insert into preguntas values('�Qui�nes son los ap�stoles a los que Jes�s llam�: hijos del trueno?','Tom�s y Mateo','Santiago y Juan','Sim�n el cananeo y Judas Iscariote','Felipe y Bartolom�','b')
insert into preguntas values('�Cu�ntos hijos tuvo Jacob?','12','6','7','11','a')
insert into preguntas values('�Cu�les fueron los ap�stoles que estuvieron con Jes�s en la transfiguraci�n?','Santiago, hijo de Alfeo, y Tadeo','Bartolom� y Juan','Felipe, Pedro y Andr�s','Pedro, Santiago y Juan','d')
insert into preguntas values('�Cu�l era el nombre de Abraham antes de su Alianza con Dios?','Aar�n','Israel','Jacob','Abram','d')
insert into preguntas values('�Qui�n perfum� los pies de Jes�s en casa de Sim�n el leproso?','Martha','Mar�a la hermana de L�zaro','Herod�as','Mar�a, su madre','b')
insert into preguntas values('Patriarca emigrante de Ur de Caldea.','Aar�n','Jacob','Isaac','Abraham','d')
insert into preguntas values('Sacerdote que ofreci� por Abraham un sacrificio de pan y vino.','Melquisedec','Potifera','Jetr�','Elcana','d')
insert into preguntas values('�Para librar a Pablo de los jud�os de Jerusal�n, que le ten�an una emboscada fue enviado de noche a Cesarea, a qu� gobernador se lo enviaron?','A Jairo','A Felipe','A F�lix','A Herodias','c')

insert into preguntas values('Primer rey de Israel.','Sa�l','Abim�lec','David','Nahas','a')
insert into preguntas values('Primer m�rtir del cristianismo.','Pedro','Esteban','Apolos','Santiago','b')
insert into preguntas values('Hijo de Abraham y de Agar.','Isaac','Jacob','Jos�','Ismael','d')
insert into preguntas values('�A qu� personas, en forma individual, les escribi� Pablo cartas?','Santiago, Juan, Timoteo','Filem�n, Timoteo, Tito','Tito, Pedro, Timoteo','Filem�n, Judas, Santiago','b')
insert into preguntas values('Rey de Israel muerto por una flecha tirada al azar.','Agag','Aqu�s','Talmai','Acab','d')
insert into preguntas values('�Qui�n es el padre de Jos� el esposo de Mar�a?','Absal�n','Jacob','Jos�as','Jecon�as','b')
insert into preguntas values('Primer sumo sacerdote de Israel.','Melquisedec','Potifera','Aar�n','Reuel','c')
insert into preguntas values('Jueces de Israel que combatieron contra S�sara.','D�bora y Barac','Lad�n y Sim�','T�rah y Nahor','Je�s y Beri�','a')
insert into preguntas values('En su discurso sobre el fin del mundo, �qu� profeta dice Jes�s que predijo la abominaci�n desoladora.','Daniel','Nat�n','Gad','Ah�as','a')
insert into preguntas values('Primer Ministro de Asuero que buscaba la muerte del t�o de Esther.','Nebuzarad�n','Ahuzat','Ficol','Am�n','d')
insert into preguntas values('�Cu�les son los disc�pulos enviados a preparar la pascua?','Andr�s y Santiago','Felipe y Bartolom�','Tom�s y Mateo','Pedro y Juan','d')
insert into preguntas values('Tercer hijo de Ad�n.','Set','En�s','Cain�n','Mahalalel','a')
insert into preguntas values('Ciego de Jeric� que a gritos decia a Jes�s: !Hijo de David, ten misericordia de m�!.','Timeo','Bartimeo','Malco','Elimas','b')
insert into preguntas values('El Fara�n so�� siete vacas gordas y...','un r�o de aguas cristalinas.','siete vacas flacas.','un lobo asechando.','siete establos.','b')
insert into preguntas values('Cuando Pedro se opone al anuncio de Jes�s de que ir�a a Jerusal�n a sufrir, �qu� duras palabras dirigi� Jes�s a Pedro?','No escuchar� m�s tus consejos.','Ap�rtate de m� Satan�s.','No tentar�s al Se�or tu Dios.','�Es que a�n no me conoces?','b')
insert into preguntas values('Completa la frase del profeta Isa�as: La virgen dar� a luz un hijo y le...','dar� gran dolor a su coraz�n.','ense�ar� el camino al Padre.','pondr� por nombre Emmanuel.','conocer� todo el mundo.','c')
insert into preguntas values('�Qu� calificativo suele dar Jes�s a los escribas y fariseos?','Codiciosos','Hip�critas','Vanidosos','Blanqueados','b')
insert into preguntas values('�Qu� pide Jes�s para los enemigos?','Amor','Comprensi�n','Castigo','Indiferencia','a')
insert into preguntas values('Completa esta sentencia de Cristo: �El que come mi cuerpo y bebe mi sangre...','Ser� fortalecido.','Me conoce a m� y yo a �l.','Entrar� al reino de los cielos.','tiene vida eterna; y yo le resucitar� en el d�a postrero.','d')
insert into preguntas values('�Qu� ejemplo usa Jes�s para decir que no hay que juzgar?','La viga y la paja en el ojo.','El rollo de la ley abierto o cerrado.','Haz el bien, sin mirar a quien.','El que es buen juez, por su casa empieza.','a')

select * from preguntas


create procedure sp_listar(
@codPreg int)
as
select * from preguntas where(@codPreg = codPreg)


create procedure sp_listarTodo
as
select * from preguntas


create procedure sp_update(
	@codPreg int,
	@preg varchar(300),
	@a varchar(200),
	@b varchar(200),
	@c varchar(200),
	@d varchar(200),
	@resp char(1))
as
	update preguntas set preg=@preg,a=@a,b=@b,
	c=@c, d=@d, resp=@resp where codPreg=@codPreg

	DROP PROCEDURE sp_update  
GO  


create procedure sp_insert01(
	@preg varchar(300),
	@a varchar(200),
	@b varchar(200),
	@c varchar(200),
	@d varchar(200),
	@resp char(1),
	@pasage varchar(50),
	@dificultad varchar(50))
	as
	insert into preguntas values(@preg, @a, @b, @c, @d, @resp, @pasage, @dificultad)

	DROP PROCEDURE sp_listarPor_Dificultad 


create procedure sp_listarPor_CodyDif(
@codPreg int,
@dificultad varchar(50))
as
select * from preguntas where(@codPreg = codPreg AND @dificultad = dificultad)

create procedure sp_listarPor_Dificultad(
@dificultad varchar(50))
as
select * from preguntas where(@dificultad = dificultad)



exec sp_update 75, 'Cuando Pedro se opone al anuncio de Jes�s de que ir�a a Jerusal�n a sufrir, �qu� duras palabras dirigi� Jes�s a Pedro?','No escuchar� m�s tus consejos.','Ap�rtate de m� Satan�s.','No tentar�s al Se�or tu Dios.','�Es que a�n no me conoces?','b'

exec sp_insert01 '�El m�s importante y el primero de los mandamientos de la ley de Mois�s?','Ama a Dios con todo tu coraz�n, con toda tu alma y con toda tu mente.','Haz el bien, sin mirar a quien.','Ama a tu pr�jimo como a ti mismo.','Vende todo y reg�lalo a los pobres.','a','Mt 22:37','Easy'

delete from preguntas where codPreg = 81

DBCC CHECKIDENT ('preguntas.codPreg', RESEED, 0)

exec sp_listarPor_Dificultad 'Easy'

select * from preguntas