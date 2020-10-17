--SELECCIONAR LA BASE DE DATOS MASTER
use master
go

-- VERIFICAR SI LA BASE DE DATOS EXISTE EN EL SYS CON
-- EL NOMBRE scc 
if exists (SELECT * FROM sys.databases where name ='scc')
	begin
		drop database scc
	end
go


--	CREAR LA BASE DE DATOS
if not exists (SELECT * FROM sys.databases where name ='scc')
	begin
		create database scc
	end 
go

-- SELECCIONAR LA BASE DE DATOS 
use scc
go


-- CREAR EL SCHEMA DE LA BASE DE DATOS
create schema scc
go

-- CREACION DE LA TABLA PACIENTES
CREATE TABLE scc.Paciente(
    idPaciente INT NOT NULL IDENTITY(100, 1) CONSTRAINT PK_Paciente_ID PRIMARY KEY CLUSTERED(idPaciente),
    nombrePaciente TEXT NOT NULL,
	identidadPaciente CHAR(13) NOT NULL,
    edad INT NOT NULL,
    sexo TEXT NOT NULL,
    escolaridad TEXT NOT NULL,
    lugarNacimiento TEXT NOT NULL,
    fechaNacimiento DATE NOT NULL,
    raza TEXT NOT NULL,
    lugarResidencia TEXT NOT NULL,
    religion TEXT NOT NULL,
    estadoCivil TEXT NOT NULL,
    correo TEXT NOT NULL,
    telefonos TEXT NOT NULL,
    direccion TEXT NOT NULL
);

-- TABLA HISTORIA
CREATE TABLE scc.Historial(
    idHistorial INT NOT NULL IDENTITY(100, 1) CONSTRAINT PK_Historial_ID PRIMARY KEY CLUSTERED(idHistorial),
    fechaIngreso DATETIME NOT NULL,
    motivoConsulta TEXT NOT NULL,
    antecedentes TEXT NOT NULL,
    descripcion TEXT NOT NULL,
    tratamiento TEXT NOT NULL,
    cita DATE NOT NULL,
    idPaciente INT NOT NULL,
    idDiagnostico INT NOT NULL,
	HEA TEXT NOT NULL,
);

-- TABLA USUARIOS
CREATE TABLE scc.Usuario(
	idUsuario INT NOT NULL IDENTITY(100,1) CONSTRAINT PK_Usuario_ID PRIMARY KEY CLUSTERED(idUsuario),
	nombreUsuario VARCHAR(255) NOT NULL,
	PASSWORD VARCHAR(255) NOT NULL
)

alter table scc.CIECAT
add primary key(ID)


-- HACER LAS RELACIONES DE LAS TABLAS
-- Tabla paciente
ALTER TABLE scc.Historial
ADD CONSTRAINT 
FK_IdPaciente$idPaciente_Paciente 
FOREIGN KEY(idPaciente) REFERENCES scc.Paciente(idPaciente)
ON UPDATE NO ACTION
ON DELETE NO ACTION;
GO



-- Tabla CIE 10
ALTER TABLE scc.Historial
ADD CONSTRAINT 
FK_IdDiagnostico$id_CIE10 
FOREIGN KEY(idDiagnostico) REFERENCES scc.CIECAT(ID)
ON UPDATE NO ACTION
ON DELETE NO ACTION;
GO







-- CHEACK CONSTRAINT

-- EL NUMERO DE IDENTIDAD ES UNICO Y NO SE 
-- DEBE DE RE
ALTER TABLE scc.Paciente
ADD CONSTRAINT AK_scc_Paciente_Identidad
UNIQUE NONCLUSTERED (identidadPaciente)
GO


-- STORE PROCEDURE DE LAS ACCIONES DE CRUD
--CRUD (Create, Read, Update, Delete)

-- PROCEDURE DE INSERTAR PACIENTE
CREATE PROCEDURE sp_AgregarPaciente(
	@nombrePaciente TEXT,
	@identidadPaciente CHAR(13),
    @edad INT,
    @sexo TEXT,
    @escolaridad TEXT,
    @lugarNacimiento TEXT,
    @fechaNacimiento DATE,
    @raza TEXT,
    @lugarResidencia TEXT,
    @religion TEXT,
    @estadoCivil TEXT,
    @correo TEXT,
    @telefonos TEXT,
    @direccion TEXT
)
AS
BEGIN
	INSERT INTO scc.Paciente(nombrePaciente, identidadPaciente, edad, sexo, escolaridad, lugarNacimiento, fechaNacimiento, raza, lugarResidencia, religion, estadoCivil, correo, telefonos, direccion)
				   VALUES (@nombrePaciente, @identidadPaciente, @edad, @sexo, @escolaridad, @lugarNacimiento, @fechaNacimiento, @raza ,@lugarResidencia, @religion, @estadoCivil, @correo, @telefonos, @direccion)
END
GO

-- Procedure para actualizar datos de un paciente
CREATE PROCEDURE sp_ActualizarPaciente(
	@nombrePaciente TEXT,
	@identidadPaciente CHAR(13),
    @edad INT,
    @sexo TEXT,
    @escolaridad TEXT,
    @lugarNacimiento TEXT,
    @fechaNacimiento DATE,
    @raza TEXT,
    @lugarResidencia TEXT,
    @religion TEXT,
    @estadoCivil TEXT,
    @correo TEXT,
    @telefonos TEXT,
    @direccion TEXT
)
AS
BEGIN
	UPDATE scc.Paciente SET nombrePaciente=@nombrePaciente, edad=@edad, sexo=@sexo, escolaridad=@escolaridad, lugarNacimiento=@lugarNacimiento, fechaNacimiento=@fechaNacimiento, raza=@raza, lugarResidencia=@lugarResidencia, religion=@religion, estadoCivil=@estadoCivil, correo=@correo, telefonos=@telefonos, direccion=@direccion
			WHERE identidadPaciente=@identidadPaciente	  
END
GO

--- FIN DEL PROCEDURE
-- PROCEDURE DE INSERTAR HISTORIA CLINICA
CREATE PROCEDURE sp_AgregarHistorial(
	@fechaIngreso DATETIME,
    @motivoConsulta TEXT,
    @antecedentes TEXT,
    @descripcion TEXT,
    @tratamiento TEXT,
    @cita DATE,
    @idPaciente INT,
    @idDiagnostico INT,
	@HEA TEXT
)
AS	
BEGIN 
	INSERT INTO scc.Historial(fechaIngreso, motivoConsulta, antecedentes, descripcion, tratamiento, cita, idPaciente , idDiagnostico, HEA)
					VALUES(@fechaIngreso, @motivoConsulta, @antecedentes, @descripcion, @tratamiento, @cita, @idPaciente, @idDiagnostico, @HEA)
END
GO

--DROP PROCEDURE sp_AgregarHistorial

select * from scc.Historial


SELECT nombrePaciente, edad, sexo, motivoConsulta, fechaIngreso, descripcion, Nombre 
	FROM scc.Paciente Paciente 
		INNER JOIN scc.Historial Historial 
			ON Paciente.idPaciente = Historial.idPaciente
		INNER JOIN scc.CIECAT Diagnostico
			ON Diagnostico.ID = Historial.idDiagnostico	
WHERE nombrePaciente Like 'Luis Dario Rodriguez Inestroza' or identidadPaciente = '';

SELECT * FROM scc.Historial;


Truncate table scc.Historial;

select * from scc.CIECAT



SELECT nombrePaciente, edad, sexo, motivoConsulta, fechaIngreso, descripcion, Nombre 
	                            FROM scc.Paciente Paciente 
		                            INNER JOIN scc.Historial Historial 
			                            ON Paciente.idPaciente = Historial.idPaciente
		                            INNER JOIN scc.CIECAT Diagnostico
			                            ON Diagnostico.ID = Historial.idDiagnostico	
                            WHERE CONVERT(varchar, nombrePaciente) LIKE '%%' and identidadPaciente = ''
GO

-- Procedeura para seleccionar un paciente especifico
CREATE PROCEDURE sp_reportePacienteUnico(
    @nombrePaciente TEXT,
    @identidadPaciente CHAR(13)
)
AS 
    BEGIN
        SELECT nombrePaciente, edad, sexo, motivoConsulta,HEA, antecedentes,tratamiento, fechaIngreso, descripcion, Nombre 
	                            FROM scc.Paciente Paciente 
		                            INNER JOIN scc.Historial Historial 
			                            ON Paciente.idPaciente = Historial.idPaciente
		                            INNER JOIN scc.CIECAT Diagnostico
			                            ON Diagnostico.ID = Historial.idDiagnostico	
                            WHERE CONVERT(varchar, nombrePaciente) LIKE @nombrePaciente or identidadPaciente = @identidadPaciente
    END 
GO

exec sp_reportePacienteUnico '', ''

DROP PROCEDURE sp_reportePacienteUnico


SELECT * FROM scc.Paciente WHERE identidadPaciente = '0318199601640'