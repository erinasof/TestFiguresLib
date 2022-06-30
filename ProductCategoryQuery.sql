CREATE TABLE product
(    
    name NVARCHAR(30) PRIMARY KEY   
)

CREATE TABLE category
(    
    name NVARCHAR(30) PRIMARY KEY  
)

CREATE TABLE product_category
(    
    product_name NVARCHAR(30) NOT NULL,
		CONSTRAINT FK_product FOREIGN KEY (product_name)
		REFERENCES product (name),
	category_name NVARCHAR(30) NOT NULL,
	CONSTRAINT FK_category FOREIGN KEY (category_name)
		REFERENCES category (name),
		CONSTRAINT PK_product_category PRIMARY KEY (product_name, category_name)
)

INSERT INTO product values ('chicken');
INSERT INTO product values ('pork');
INSERT INTO product values ('onion');
INSERT INTO product values ('calendar');
INSERT INTO category values ('meat');
INSERT INTO category values ('first need');
INSERT INTO category values ('vegetable');
INSERT INTO category values ('sweet');
INSERT INTO product_category values ('chicken', 'meat');
INSERT INTO product_category values ('pork', 'meat');
INSERT INTO product_category values ('onion', 'vegetable');
INSERT INTO product_category values ('chicken', 'first need');
INSERT INTO product_category values ('onion', 'first need');



SELECT product.name, category.name FROM product
LEFT JOIN product_category ON product.name = product_category.product_name
LEFT JOIN category ON product_category.category_name = category.name;