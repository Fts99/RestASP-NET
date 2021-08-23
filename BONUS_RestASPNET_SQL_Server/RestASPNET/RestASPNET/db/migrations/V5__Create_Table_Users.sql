CREATE TABLE users (
	id bigint identity,
	user_name VARCHAR(50) NOT NULL,
	password VARCHAR(130) NOT NULL,
	full_name VARCHAR(120) NOT NULL,
	refresh_token VARCHAR(500) NULL DEFAULT '0',
	refresh_token_expiry_time DATETIME DEFAULT NULL,
	PRIMARY KEY (id),
	UNIQUE (user_name)
);