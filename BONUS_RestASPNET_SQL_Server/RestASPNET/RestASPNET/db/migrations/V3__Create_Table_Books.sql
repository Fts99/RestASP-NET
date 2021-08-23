CREATE TABLE books (
  id bigint PRIMARY KEY identity,
  author varchar(255),
  launch_date datetime NOT NULL,
  price decimal(30,2) NOT NULL,
  title varchar(255)
);