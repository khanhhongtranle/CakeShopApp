BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "kindofcakes" (
	"id"	INTEGER,
	"name"	TEXT,
	PRIMARY KEY("id")
);
CREATE TABLE IF NOT EXISTS "cakes" (
	"id"	TEXT,
	"name"	TEXT,
	"description"	TEXT,
	"date_entered"	DATETIME,
	"kindofcake_id"	int,
	"unit_price"	INTEGER,
	PRIMARY KEY("id")
);
CREATE TABLE IF NOT EXISTS "orders" (
	"id"	TEXT,
	"date_entered"	DATETIME,
	"total"	INTEGER,
	PRIMARY KEY("id")
);
CREATE TABLE IF NOT EXISTS "cake_img" (
	"cake_id"	TEXT,
	"img_id"	TEXT
);
CREATE TABLE IF NOT EXISTS "images" (
	"id"	TEXT,
	"link"	TEXT,
	PRIMARY KEY("id")
);
CREATE TABLE IF NOT EXISTS "order_cake" (
	"order_id"	TEXT,
	"cake_id"	TEXT,
	"quantity"	INTEGER,
	"price"	INTEGER,
	"amount"	INTEGER
);
INSERT INTO "kindofcakes" VALUES (1,'Cake');
INSERT INTO "kindofcakes" VALUES (2,'Bread');
INSERT INTO "kindofcakes" VALUES (3,'Gift');
INSERT INTO "kindofcakes" VALUES (4,'Snack');
INSERT INTO "cakes" VALUES ('12/28/2020 2:10:09 PM','Corn Cheese Mousse Cake','Bánh kem nhân bắp','12/28/2020 2:10:09 PM',1,490000);
INSERT INTO "cakes" VALUES ('12/31/2020 7:18:35 AM','Fruit Red Jewelry Moussee','Bánh kem vị dâu và phô mai','12/31/2020 7:18:35 AM',1,400000);
INSERT INTO "orders" VALUES ('12/31/2020 7:04:54 AM','12/31/2020 7:04:54 AM',1470000);
INSERT INTO "cake_img" VALUES ('12/28/2020 2:10:09 PM','Corn Cheese Mousse CakeImages\corn-cheese-mousse-cake.jpeg');
INSERT INTO "cake_img" VALUES ('12/28/2020 2:10:09 PM','Corn Cheese Mousse CakeImages\corn-cheese-mousse-cake(copy1).jpeg');
INSERT INTO "cake_img" VALUES ('12/31/2020 7:18:35 AM','Fruit Red Jewelry MousseeImages\48ad4087-643a-45e8-9402-5e23aaff9eec.jpg');
INSERT INTO "cake_img" VALUES ('12/31/2020 7:18:35 AM','Fruit Red Jewelry MousseeImages\48ad4087-643a-45e8-9402-5e23aaff9eec(copy1).jpg');
INSERT INTO "cake_img" VALUES ('12/31/2020 7:18:35 AM','Fruit Red Jewelry MousseeImages\48ad4087-643a-45e8-9402-5e23aaff9eec(copy2).jpg');
INSERT INTO "images" VALUES ('Corn Cheese Mousse CakeImages\corn-cheese-mousse-cake.jpeg','Images\corn-cheese-mousse-cake.jpeg');
INSERT INTO "images" VALUES ('Corn Cheese Mousse CakeImages\corn-cheese-mousse-cake(copy1).jpeg','Images\corn-cheese-mousse-cake(copy1).jpeg');
INSERT INTO "images" VALUES ('Fruit Red Jewelry Moussee','');
INSERT INTO "images" VALUES ('Fruit Red Jewelry MousseeImages\48ad4087-643a-45e8-9402-5e23aaff9eec(copy1).jpg','Images\48ad4087-643a-45e8-9402-5e23aaff9eec(copy1).jpg');
INSERT INTO "images" VALUES ('Fruit Red Jewelry MousseeImages\48ad4087-643a-45e8-9402-5e23aaff9eec(copy2).jpg','Images\48ad4087-643a-45e8-9402-5e23aaff9eec(copy2).jpg');
INSERT INTO "order_cake" VALUES ('12/31/2020 7:04:54 AM','12/28/2020 2:10:09 PM',3,490000,1470000);
COMMIT;
