CREATE TABLE Users (
	name VARCHAR(32),
	average_stars FLOAT,
	fans INTEGER,
	cool INTEGER,
	review_count INTEGER,
	funny INTEGER,
	yelping_since DATE,
	useful INTEGER,
	user_id CHAR(22) PRIMARY KEY
);

CREATE TABLE Friends (
	user1 CHAR(22),
	user2 CHAR(22),
	PRIMARY KEY(user1,user2),
	FOREIGN KEY(user1) REFERENCES Users(user_id),
	FOREIGN KEY(user2) REFERENCES Users(user_id)
);

CREATE TABLE Reviews (
	review_stars INTEGER,
	review_date DATE,
	text VARCHAR(300),
	useful_vote INTEGER,
	funny_vote INTEGER,
	cool_vote INTEGER,
	review_id CHAR(22) PRIMARY KEY,
	user_id CHAR(22) NOT NULL,
	FOREIGN KEY (user_id) REFERENCES Users(user_id)
);

CREATE TABLE Business (
	zipcode CHAR(5),
	review_count INTEGER,
	name VARCHAR(32),
	latitude FLOAT,
	longitude FLOAT,
	city VARCHAR(32),
	b_state CHAR(2),
	address VARCHAR(32),
	is_open BOOLEAN,
	stars FLOAT,
	num_checkins INTEGER,
	review_rating FLOAT,
	business_id CHAR(22) PRIMARY KEY
);

CREATE TABLE Categories (
	category_name VARCHAR(32),
	business_id CHAR(22),
	PRIMARY KEY(business_id, category_name),
	FOREIGN KEY(business_id) REFERENCES Business(business_id)
);

CREATE TABLE Attributes (
	a_value VARCHAR(16),
	a_name VARCHAR(16),
	business_id CHAR(22),
	PRIMARY KEY(business_id, a_name),
	FOREIGN KEY(business_id) REFERENCES Business(business_id)
);

CREATE TABLE Hours (
	h_open VARCHAR(5),
	h_close VARCHAR(5),
	h_day VARCHAR(9),
	business_id CHAR(22),
	PRIMARY KEY(business_id, h_day),
	FOREIGN KEY(business_id) REFERENCES Business(business_id)
);

CREATE TABLE Checkins (
	c_count INTEGER,
	c_day VARCHAR(9),
	c_time VARCHAR(5),
	business_id CHAR(22),
	PRIMARY KEY(business_id, c_day, c_time),
	FOREIGN KEY(business_id) REFERENCES Business(business_id)
);
	