-- Zelimir Maletic
-- Projektni zadatak - Biblioteka
-- Interakcija covjek-racunar 2020
-- ETF Banja Luka

drop schema  if exists  biblioteka;
create schema biblioteka;
use biblioteka;

create table Mjesto(
    IdMjesto int unsigned auto_increment not null,
    Naziv varchar(45) not null ,
    PostanskiBroj varchar(10) unique not null ,
    -- KEYS
    primary key (IdMjesto)
)engine = InnoDB;

create table Izdavac(
    IdIzdavac int unsigned auto_increment not null ,
    IdMjesto int unsigned not null ,
    Naziv varchar(45) not null ,
    Adresa varchar(45) not null ,
    -- KEYS
    primary key (IdIzdavac),
    foreign key (IdMjesto)
        references Mjesto(IdMjesto)
)engine = InnoDB;

create table Zanr(
    IdZanr int unsigned auto_increment not null ,
    Naziv varchar(45) not null ,
    Opis text(250),
    -- KEYS
    primary key (IdZanr)
)engine = InnoDB;

create table Autor(
    IdAutor int unsigned auto_increment not null ,
    IdMjesto int unsigned not null ,
    Ime varchar(45) not null ,
    Prezime varchar(45) not null ,
    DatumRodjenja int,
    -- KEYS
    primary key (IdAutor),
    foreign key (IdMjesto)
        references Mjesto(IdMjesto)
)engine = InnoDB;

create table Knjiga(
    IdKnjiga int unsigned auto_increment not null ,
    IdZanr int unsigned not null ,
    IdIzdavac int unsigned not null ,
    Naslov varchar(45) not null ,
    DatumObjavljivanja date not null ,
    ISBN varchar(30) not null ,
    UkupanBrojKopija int not null ,
    BrojStranica int not null ,
    Jezik varchar(20),
    Opis text(500),
    -- KEYS
    primary key (IdKnjiga),
    foreign key (IdZanr)
        references Zanr(IdZanr),
    foreign key (IdIzdavac)
        references Izdavac(IdIzdavac)
)engine = InnoDB;

create table Knjiga_ima_Autor(
    IdKnjiga int unsigned not null ,
    IdAutor int unsigned not null ,
    -- KEYS
    primary key (IdKnjiga, IdAutor),
    foreign key (IdAutor)
        references Autor(IdAutor),
    foreign key (IdKnjiga)
        references Knjiga(IdKnjiga)
)engine = InnoDB;

create table Osoba(
    IdOsoba int unsigned auto_increment not null ,
    IdMjesto int unsigned not null ,
    Ime varchar(45) not null ,
    Prezime varchar(45) not null ,
    Adresa varchar(45) not null ,
    BrojTelefona varchar(15),
    -- KYES
    primary key (IdOsoba),
    foreign key (IdMjesto)
        references Mjesto(IdMjesto)
)engine = InnoDB;

create table Administrator(
    IdAdministrator int unsigned not null ,
    KorisnickoIme varchar(45) not null unique ,
    Lozinka varchar(45) not null ,
    -- KEYS
    primary key (IdAdministrator),
    foreign key (IdAdministrator)
        references Osoba(IdOsoba)
)engine = InnoDB;

create table Bibliotekar(
    IdBibliotekar int unsigned not null ,
    KorisnickoIme varchar(45) not null unique ,
    Lozinka varchar(45) not null ,
    -- KEYS
    primary key (IdBibliotekar),
    foreign key (IdBibliotekar)
        references Osoba(IdOsoba)
)engine = InnoDB;

create table Clan(
    IdClan int unsigned not null ,
    DatumUclanjivanja date not null ,
    DatumObnavljanjaClanstva date not null ,
    Email varchar(45),
    -- KEYS
    primary key (IdClan),
    foreign key (IdClan)
        references Osoba(IdOsoba)
)engine = InnoDB;

create table Pozajmica(
    IdPozajmica int unsigned auto_increment not null ,
    IdClan int unsigned not null ,
    IdKnjiga int unsigned not null ,
    IdBibliotekar int unsigned not null ,
    DatumPozajmljivanja date not null ,
    Razduzena tinyint not null ,
    Opis text(150),
    -- KEYS
    primary key (IdPozajmica),
    foreign key (IdClan)
        references Clan(IdClan),
    foreign key (IdKnjiga)
        references Knjiga(IdKnjiga),
    foreign key (IdBibliotekar)
        references Bibliotekar(IdBibliotekar)
)engine = InnoDB;