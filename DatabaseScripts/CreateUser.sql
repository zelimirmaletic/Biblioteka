CREATE USER 'korisnikBiblioteke'@'localhost' IDENTIFIED BY 'biblioteka';
GRANT ALL PRIVILEGES ON biblioteka . * TO 'korisnikBiblioteke'@'localhost';
FLUSH PRIVILEGES;
