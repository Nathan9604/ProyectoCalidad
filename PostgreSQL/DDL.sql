CREATE TABLE Aeropuerto(
	Codigo				VARCHAR(3)		PRIMARY KEY,
	Nombre				VARCHAR(16)		NOT NULL,
	Direccion			VARCHAR(128),
	Habilitado			BOOLEAN			NOT NULL
);

CREATE TABLE Ruta(
	Codigo				VARCHAR(6)		PRIMARY KEY,
	Empresa				VARCHAR(16)		NOT NULL,
	Hora				TIME			NOT NULL,
	Estado				BOOLEAN			NOT NULL,
	Lugar				VARCHAR(32)		NOT NULL,
	CapacidadMaxima			INTEGER			NOT NULL,
	CodigoAeropuerto		VARCHAR(6),
	
	FOREIGN KEY (CodigoAeropuerto) REFERENCES Aeropuerto(Codigo)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);

CREATE TABLE Vuelo(
	CodigoRuta			VARCHAR(6),
	Fecha				DATE			NOT NULL,
	CapacidadReal			INTEGER			NOT NULL,
	
	PRIMARY KEY (CodigoRuta,Fecha),
	FOREIGN KEY (CodigoRuta) REFERENCES Ruta(Codigo)
		ON DELETE CASCADE
		ON UPDATE CASCADE
);
