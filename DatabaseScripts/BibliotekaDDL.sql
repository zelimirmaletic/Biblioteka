-- Zelimir Maletic
-- Projektni zadatak - Biblioteka
-- Interakcija covjek-racunar 2020
-- ETF Banja Luka

drop schema  if exists  biblioteka;
create schema biblioteka;
use biblioteka;

create table Mjesto(
    IdMjesto int unsigned auto_increment,
    Naziv varchar(50) not null ,
    PostanskiBroj varchar(10) unique not null ,
    -- KEYS
    primary key (IdMjesto)
)engine = InnoDB;
create index UniqueIndex_Mjesto_Naziv on Mjesto
(
	Naziv asc
);

create table Izdavac(
    IdIzdavac int(5) zerofill auto_increment,
    IdMjesto int unsigned not null ,
    Naziv varchar(45) not null ,
    Adresa varchar(45) not null ,
    -- KEYS
    primary key (IdIzdavac),
    foreign key (IdMjesto)
        references Mjesto(IdMjesto)
		on update cascade on delete restrict
)engine = InnoDB;
create index Index_Izdavac_Naziv on Izdavac
(
	Naziv asc
);

create table Zanr(
    Naziv varchar(45) unique,
    Opis text(250),
    -- KEYS
    primary key (Naziv)
)engine = InnoDB;
create unique index UniqueIndex_Zanr_Naziv on Zanr
(
	Naziv asc
);

create table Autor(
    IdAutor int unsigned auto_increment,
    IdMjesto int unsigned not null ,
    Ime varchar(45) not null ,
    Prezime varchar(45) not null ,
    DatumRodjenja date,
    -- KEYS
    primary key (IdAutor),
    foreign key (IdMjesto)
        references Mjesto(IdMjesto)
		on update cascade on delete restrict
)engine = InnoDB;
create index Index_Autor_Prezime_Ime on Autor
(
	Prezime asc, Ime asc
);

create table Knjiga(
    IdKnjiga int(7) zerofill auto_increment not null ,
    NazivZanra varchar(45) not null ,
    IdIzdavac int unsigned not null ,
    IdAutor int unsigned not null,
    Naslov varchar(45) not null ,
    DatumObjavljivanja date not null ,
    ISBN varchar(30) not null ,
    UkupanBrojKopija int unsigned not null ,
    BrojStranica int unsigned not null ,
    Jezik varchar(20),
    Opis text(500),
    -- KEYS
    primary key (IdKnjiga),
    foreign key (NazivZanra)
        references Zanr(Naziv)
		on update cascade on delete restrict,
    foreign key (IdIzdavac)
        references Izdavac(IdIzdavac)
		on update cascade on delete restrict,
	foreign key (IdAutor)
		references Autor(IdAutor)
		on update cascade on delete restrict
)engine = InnoDB;
create unique index Index_Knjiga_NazivKnjige on Knjiga
(
	Naslov asc
);

create table Osoba(
    IdOsoba int unsigned auto_increment,
    NazivMjesta varchar(45) not null ,
    Ime varchar(45) not null ,
    Prezime varchar(45) not null ,
    Adresa varchar(45) not null ,
    BrojTelefona varchar(15) not null,
	Email varchar(45) not null,
	DatumRodjenja date not null,
    -- KYES
    primary key (IdOsoba),
    foreign key (NazivMjesta)
        references Mjesto(Naziv)
		on update cascade on delete restrict
)engine = InnoDB;
create index Index_Osoba_Ime_Prezime on Osoba
(
	Ime asc, Prezime asc
);

create table Administrator(
    IdAdministrator int unsigned,
    KorisnickoIme varchar(45) not null unique ,
    Lozinka varchar(45) not null ,
    -- KEYS
    primary key (IdAdministrator),
    foreign key (IdAdministrator)
        references Osoba(IdOsoba)
		on update cascade on delete restrict
)engine = InnoDB;

create table Bibliotekar(
    IdBibliotekar int unsigned,
    KorisnickoIme varchar(45) not null unique ,
    Lozinka varchar(45) not null ,
    -- KEYS
    primary key (IdBibliotekar),
    foreign key (IdBibliotekar)
        references Osoba(IdOsoba)
		on update cascade on delete restrict
)engine = InnoDB;

create table Clan(
    IdClan int unsigned,
    DatumUclanjivanja date not null,
    DatumObnavljanjaClanstva date not null,
    -- KEYS
    primary key (IdClan),
    foreign key (IdClan)
        references Osoba(IdOsoba)
		on update cascade on delete restrict
)engine = InnoDB;

create table Obavjestenje(
	IdObavjestenje int unsigned auto_increment,
    IdAdministrator int unsigned,
    IdBibliotekar int unsigned,
    Naslov varchar(45) not null,
    Datum datetime not null,
    Tekst text(500),
    ZaSve tinyint not null,
    -- KEYS
    primary key(IdObavjestenje),
    foreign key (IdAdministrator)
		references Administrator(IdAdministrator)
        on update cascade on delete restrict,
	foreign key (IdBibliotekar)
		references Bibliotekar(IdBibliotekar)
        on update cascade on delete restrict
)engine=InnoDB;

create table Tema (
	IdTema int unsigned auto_increment,
    IdOsoba int unsigned,
    Stil int(2) unsigned zerofill default 1 not null,
    -- KEYS
    primary key (IdTema),
    foreign key (IdOsoba)
		references Osoba(IdOsoba)
        on update cascade on delete restrict
)engine=InnoDB;

create table Pozajmica(
    IdPozajmica int zerofill unsigned auto_increment ,
    IdClan int unsigned not null ,
    IdKnjiga int unsigned not null ,
    IdBibliotekar int unsigned not null ,
    DatumPozajmljivanja date not null ,
    JeRazduzena tinyint default false not null ,
    Opis text(150),
    -- KEYS
    primary key (IdPozajmica),
    foreign key (IdClan)
        references Clan(IdClan)
		on update cascade on delete restrict,
    foreign key (IdKnjiga)
        references Knjiga(IdKnjiga)
		on update cascade on delete restrict,
    foreign key (IdBibliotekar)
        references Bibliotekar(IdBibliotekar)
		on update cascade on delete restrict
)engine = InnoDB;
create index Index_Pozajmica_Datum on Pozajmica
(
	DatumPozajmljivanja desc
);