CREATE TABLE customers (
    customer_id SERIAL PRIMARY KEY,
    username VARCHAR(50) UNIQUE NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    address VARCHAR(255),  -- Поле для хранения адреса
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE products (
    product_id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    description TEXT,
    price DECIMAL(10, 2) NOT NULL,
    image_url VARCHAR(255),  -- Поле для хранения ссылки на изображение продукта
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE cart (
    cart_id SERIAL PRIMARY KEY,
    customer_id INT NOT NULL REFERENCES customers(customer_id) ON DELETE CASCADE,
    product_id INT NOT NULL REFERENCES products(product_id) ON DELETE CASCADE,
    quantity INT NOT NULL DEFAULT 1,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

ALTER TABLE cart
ADD CONSTRAINT unique_customer_product UNIQUE (customer_id, product_id);


CREATE TABLE orders (
    order_id SERIAL PRIMARY KEY,
    customer_id INT NOT NULL REFERENCES customers(customer_id) ON DELETE CASCADE,
    order_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    total_amount DECIMAL(10, 2) NOT NULL,
    status VARCHAR(50) NOT NULL DEFAULT 'pending'
);


--- 
INSERT INTO products (product_id, name, price, image_url) VALUES
(1, 'Кольцо', 500, 'https://i.pinimg.com/originals/ab/1d/07/ab1d071caa9fb4b8e4de50121008661c.jpg'),
(2, 'Серьги', 1000, 'https://i.pinimg.com/736x/60/f0/71/60f07112176bb62889670bdd15e87423.jpg'),
(3, 'Браслет', 750, 'https://i.pinimg.com/originals/68/09/e7/6809e7bb8c8ca15cdd8dcd2823c608a6.jpg');

