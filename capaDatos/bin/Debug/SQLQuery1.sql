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



insert into preguntas values('¿Quién fue el primer rey de Israel?','David','Aarón','Saúl','Moisés','c')
insert into preguntas values('¿Con que rey se casó Esther para convertirse en reina?','Darío','Asuero','Belsasar','Herodes','b')
insert into preguntas values('¿Quién sirvió como profeta en el reinado del rey Darío el persa?','Zacarías','Hageo','Daniel','Jeremías','c')
insert into preguntas values('¿Quién deseaba la viña de Nabot tanto como para poder dormir?','Acab','Jeroboam','Jezabel','Manases','a')

insert into preguntas values('¿En qué día de la creación Dios hizo los animales?','Segundo','Tercero','Cuarto','Quinto','d')
insert into preguntas values('¿Qué general del ejército sirio fue leproso?','Naamán','Urías','Acan','Jefte','a')
insert into preguntas values('¿Cuantos discípulos estuvieron presentes en la transfiguración?','Tres','Dos','Cuatro','Cinco','a')
insert into preguntas values('¿Cómo se llamaba el Padre de Juan el bautista?','Jacobo','Zacarías','Ezequías','Eli','b')
insert into preguntas values('¿En que se originó el pecado?','Adán','Caín','Eva','Lucifer','d')
insert into preguntas values('¿Cuál fue el nombre que Nabuconosor le puso a Azarías?','Sadrac','Mesac','Abed-Nego','Beltsasar','c')
insert into preguntas values('¿En la transfiguración de Jesús, este apareció junto a Moisés y','Eliseo','Enoc','Elías','Juan el Bautista','c')
insert into preguntas values('¿Quién fue el primogénito de Adán y Eva?','Cain','Abel','Set','Noe','a')
insert into preguntas values('¿Quién fue el padre de Samuel?','Eliu','Esaú','Elcana','Eli','c')
insert into preguntas values('¿Cuantas monedas le dieron a Judas por entregar a Jesús?','40 ','30','20','50','b')
insert into preguntas values('¿En qué día de la creación hizo Dios los árboles y las plantas?','Primero','Segundo','Tercero','Cuarto','c')
insert into preguntas values('¿Bienaventurados los pobres en espíritu porque','Ellos verán a Dios','De ellos es el reino','Ellos serán saciados','Ellos serán consolados','b')
insert into preguntas values('¿Qué pasaje del antiguo testamento leía el etíope cuando se encontró con Felipe?','Daniel 2','Éxodo 20','Isaías 53','Salmo 23','c')
insert into preguntas values('¿En qué día de la creación hizo Dios al hombre?','Cuarto','Quinto','Sexto','Tercero','c')
insert into preguntas values('¿Quiénes fueron los discípulos que le dieron a Jesús “Concédenos que en tu gloria nos sentemos uno a la derecha y el otro a tu izquierda”?','Andrés y Juan','Juan y Pedro','Andrés y Jacobo','Jacobo y juan','d')
insert into preguntas values('¿En la parábola de las diez vírgenes. ¿Cuántas se durmieron? ','Cinco','Una','Diez','Dos','c')
insert into preguntas values('¿En qué día de la creación separo Dios la luz de la oscuridad?','Primero','Segundo','Tercero','Cuarto','a')
insert into preguntas values('¿Cómo eran los cabellos del personaje que mira el apóstol Juan en Apocalipsis cap. 1?','Dorados como el oro','Como bronce Bruñido','Blancos como la lana','Gris como la Espinela','c')
insert into preguntas values('¿En apocalipsis que simbolizan las siete estrellas en la diestra de Jesús?','Las 7 Iglesias','Los 7 Ángeles','Los 7 Juicios','Los 7 Espíritus','b')
insert into preguntas values('¿En las bodas de Cana cuantas tinajas de piedra llenaron de agua?','4','5','6','7','c')
insert into preguntas values('¿Con que dio de comer Jesús a 5000 personas?','5 panes y 2 peces','7 panes y 3 peces','5 peces y 2 panes','4 panes y 3 peces','a')
insert into preguntas values('¿Cuál es el sexto mandamiento?','No tendrás dioses ajenos','No Hurtaras','No Mataras','No Codiciaras','c')
insert into preguntas values('¿Qué libro relata la edificación de los muros de Jerusalén?','Jeremías','Isaías','Lamentaciones','Nehemías','d')
insert into preguntas values('¿En qué libro se encuentra la profecía del gran sufrimiento de Jesús?','Abdías','Génesis','Jeremías','Isaías','d')
insert into preguntas values('¿Quién dijo, apártate de mi Señor porque soy hombre pecador?','Pablo','Pedro','Andrés','Mateo','b')
insert into preguntas values('¿Cómo fueron llamados los jefes de Israel después de la muerte de Josué?','Reyes','Sacerdotes','Jueces','Levitas','c')

insert into preguntas values('¿Por cuantos años estuvieron los judíos en Egipto?','390','430','420','410','d')
insert into preguntas values('¿Quién fue bendecido por tener en su casa el arca del Señor?','David','Saul','Odeb-Edom','Obeb-Tubal','c')
insert into preguntas values('¿De quién era esposa Herodías?','Herodes','Felipe','Agripa','Pilato','c')
insert into preguntas values('¿Durante el reinado de quien se halló el libro de la ley que por mucho tiempo estaba perdido?','Salomón','Ezequías','Isaías','Josías','d')
insert into preguntas values('¿A cuántos profetas de Baal degolló Elías?','400','420','430','450','d')
insert into preguntas values('¿A cuántos hombres alimento Eliseo con 20 panes de cebada?','100','200','300','5000','a')
insert into preguntas values('¿Cómo se llamó el rey que vio una mano escribiendo en la pared?','Nabucodonosor','Daniel','Belsasar','Ciro','c')
insert into preguntas values('¿Quién estuvo 3 días y tres noches en el vientre de un pez?','Sofonías','Arquelao','Jonás','Mateo','c')
insert into preguntas values('¿Qué hecho convirtió a David en héroe para Israel?','Su victoria sobre Goliat','Sus poesías','Su victoria sobre los babilonios','Su victoria sobre Jericó','a')
insert into preguntas values('¿Quién fue arrojado al foso de los leones y salió intacto de ahí?','Juan Bautista','Daniel','Moisés','Pablo','b')
insert into preguntas values('¿A quién reclama Jesús en Getsemaní: "¿Ni siquiera una hora pudiste mantener despierto?"','Juan','Pedro','Andrés','Judas Tadeo','b')
insert into preguntas values('¿Hijo de Abraham que estuvo a punto de ser sacrificado?','Esaú','Jonás','José','Isaac','d')
insert into preguntas values('Apóstol que había sido cobrador de impuestos para Roma.','Marcos','Mateo','Lucas','Juan','b')
insert into preguntas values('Junto con José estaban en la cárcel el _____ y el _____ del faraón.','Amigo y el enemigo','Hermano y el primo','Copero y el Panadero','General y el consejero','c')
insert into preguntas values('Rey al que los magos visitaron en Judea para preguntar por el Mesías.','David','Herodes','Cesar Augusto','Agripa','b')
insert into preguntas values('Primer hijo de Adán y Eva','Judas Macabeo','Abel','Daniel','Caín','d')
insert into preguntas values('¿A la hija de qué jefe de la sinagoga resucita Cristo?','Jairo','Anás','Zacarías','Elí','a')
insert into preguntas values('Quién sucede a David en el trono de Israel.','Miqueas','Salomón','Roboam','Jeroboam','b')
insert into preguntas values('¿Qué profeta sintió que la burra le hablaba?','Elias','Isaias','Zacarías','Balaam','d')
insert into preguntas values('Rey de Babilonia que fabricó una estatua de sí y exigió adorarla.','Nabucodonosor','Ciro','Ramsés','Sihón','a')
insert into preguntas values('Apóstol elegido para reemplazar a Judas.','Matías','Bernabé','Apolos','Silas','a')
insert into preguntas values('Nuera de Noemí que al morir el esposo la acompañó hasta Belén. Un libro del A. T. lleva su nombre.','Ester','Judit','Rut','Rebeca','c')
insert into preguntas values('¿Quiénes son los apóstoles a los que Jesús llamó: hijos del trueno?','Tomás y Mateo','Santiago y Juan','Simón el cananeo y Judas Iscariote','Felipe y Bartolomé','b')
insert into preguntas values('¿Cuántos hijos tuvo Jacob?','12','6','7','11','a')
insert into preguntas values('¿Cuáles fueron los apóstoles que estuvieron con Jesús en la transfiguración?','Santiago, hijo de Alfeo, y Tadeo','Bartolomé y Juan','Felipe, Pedro y Andrés','Pedro, Santiago y Juan','d')
insert into preguntas values('¿Cuál era el nombre de Abraham antes de su Alianza con Dios?','Aarón','Israel','Jacob','Abram','d')
insert into preguntas values('¿Quién perfumó los pies de Jesús en casa de Simón el leproso?','Martha','María la hermana de Lázaro','Herodías','María, su madre','b')
insert into preguntas values('Patriarca emigrante de Ur de Caldea.','Aarón','Jacob','Isaac','Abraham','d')
insert into preguntas values('Sacerdote que ofreció por Abraham un sacrificio de pan y vino.','Melquisedec','Potifera','Jetró','Elcana','d')
insert into preguntas values('¿Para librar a Pablo de los judíos de Jerusalén, que le tenían una emboscada fue enviado de noche a Cesarea, a qué gobernador se lo enviaron?','A Jairo','A Felipe','A Félix','A Herodias','c')

insert into preguntas values('Primer rey de Israel.','Saúl','Abimélec','David','Nahas','a')
insert into preguntas values('Primer mártir del cristianismo.','Pedro','Esteban','Apolos','Santiago','b')
insert into preguntas values('Hijo de Abraham y de Agar.','Isaac','Jacob','José','Ismael','d')
insert into preguntas values('¿A qué personas, en forma individual, les escribió Pablo cartas?','Santiago, Juan, Timoteo','Filemón, Timoteo, Tito','Tito, Pedro, Timoteo','Filemón, Judas, Santiago','b')
insert into preguntas values('Rey de Israel muerto por una flecha tirada al azar.','Agag','Aquís','Talmai','Acab','d')
insert into preguntas values('¿Quién es el padre de José el esposo de María?','Absalón','Jacob','Josías','Jeconías','b')
insert into preguntas values('Primer sumo sacerdote de Israel.','Melquisedec','Potifera','Aarón','Reuel','c')
insert into preguntas values('Jueces de Israel que combatieron contra Sísara.','Débora y Barac','Ladán y Simí','Térah y Nahor','Jeús y Beriá','a')
insert into preguntas values('En su discurso sobre el fin del mundo, ¿qué profeta dice Jesús que predijo la abominación desoladora.','Daniel','Natán','Gad','Ahías','a')
insert into preguntas values('Primer Ministro de Asuero que buscaba la muerte del tío de Esther.','Nebuzaradán','Ahuzat','Ficol','Amán','d')
insert into preguntas values('¿Cuáles son los discípulos enviados a preparar la pascua?','Andrés y Santiago','Felipe y Bartolomé','Tomás y Mateo','Pedro y Juan','d')
insert into preguntas values('Tercer hijo de Adán.','Set','Enós','Cainán','Mahalalel','a')
insert into preguntas values('Ciego de Jericó que a gritos decia a Jesús: !Hijo de David, ten misericordia de mí!.','Timeo','Bartimeo','Malco','Elimas','b')
insert into preguntas values('El Faraón soñó siete vacas gordas y...','un río de aguas cristalinas.','siete vacas flacas.','un lobo asechando.','siete establos.','b')
insert into preguntas values('Cuando Pedro se opone al anuncio de Jesús de que iría a Jerusalén a sufrir, ¿qué duras palabras dirigió Jesús a Pedro?','No escucharé más tus consejos.','Apártate de mí Satanás.','No tentarás al Señor tu Dios.','¿Es que aún no me conoces?','b')
insert into preguntas values('Completa la frase del profeta Isaías: La virgen dará a luz un hijo y le...','dará gran dolor a su corazón.','enseñará el camino al Padre.','pondrá por nombre Emmanuel.','conocerá todo el mundo.','c')
insert into preguntas values('¿Qué calificativo suele dar Jesús a los escribas y fariseos?','Codiciosos','Hipócritas','Vanidosos','Blanqueados','b')
insert into preguntas values('¿Qué pide Jesús para los enemigos?','Amor','Comprensión','Castigo','Indiferencia','a')
insert into preguntas values('Completa esta sentencia de Cristo: “El que come mi cuerpo y bebe mi sangre...','Será fortalecido.','Me conoce a mí y yo a él.','Entrará al reino de los cielos.','tiene vida eterna; y yo le resucitaré en el día postrero.','d')
insert into preguntas values('¿Qué ejemplo usa Jesús para decir que no hay que juzgar?','La viga y la paja en el ojo.','El rollo de la ley abierto o cerrado.','Haz el bien, sin mirar a quien.','El que es buen juez, por su casa empieza.','a')

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



exec sp_update 75, 'Cuando Pedro se opone al anuncio de Jesús de que iría a Jerusalén a sufrir, ¿qué duras palabras dirigió Jesús a Pedro?','No escucharé más tus consejos.','Apártate de mí Satanás.','No tentarás al Señor tu Dios.','¿Es que aún no me conoces?','b'

exec sp_insert01 '¿El más importante y el primero de los mandamientos de la ley de Moisés?','Ama a Dios con todo tu corazón, con toda tu alma y con toda tu mente.','Haz el bien, sin mirar a quien.','Ama a tu prójimo como a ti mismo.','Vende todo y regálalo a los pobres.','a','Mt 22:37','Easy'

delete from preguntas where codPreg = 81

DBCC CHECKIDENT ('preguntas.codPreg', RESEED, 0)

exec sp_listarPor_Dificultad 'Easy'

select * from preguntas