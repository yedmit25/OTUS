-- public.cart определение

-- Drop table

-- DROP TABLE public.cart;

CREATE TABLE public.cart (
  cart_id serial4 NOT NULL,
  customer_id int4 NOT NULL,
  product_id int4 NOT NULL,
  quantity int4 DEFAULT 1 NOT NULL,
  created_at timestamp DEFAULT CURRENT_TIMESTAMP NULL,
  CONSTRAINT cart_pkey PRIMARY KEY (cart_id),
  CONSTRAINT unique_customer_product null
);

-- Permissions

ALTER TABLE public.cart OWNER TO postgres;
GRANT ALL ON TABLE public.cart TO postgres;


-- public.cart внешние включи

ALTER TABLE public.cart ADD CONSTRAINT cart_customer_id_fkey FOREIGN KEY (customer_id) REFERENCES public.customers(customer_id) ON DELETE CASCADE;
ALTER TABLE public.cart ADD CONSTRAINT cart_product_id_fkey FOREIGN KEY (product_id) REFERENCES public.products(product_id) ON DELETE CASCADE;


-- public.customers определение

-- Drop table

-- DROP TABLE public.customers;

CREATE TABLE public.customers (
  customer_id serial4 NOT NULL,
  username varchar(50) NOT NULL,
  email varchar(100) NOT NULL,
  address varchar(255) NULL,
  created_at timestamp DEFAULT CURRENT_TIMESTAMP NULL,
  updated_at timestamp DEFAULT CURRENT_TIMESTAMP NULL,
  CONSTRAINT customers_email_key UNIQUE (email),
  CONSTRAINT customers_pkey PRIMARY KEY (customer_id),
  CONSTRAINT customers_username_key UNIQUE (username)
);

-- Permissions

ALTER TABLE public.customers OWNER TO postgres;
GRANT ALL ON TABLE public.customers TO postgres;

-- public.orders определение

-- Drop table

-- DROP TABLE public.orders;

CREATE TABLE public.orders (
  order_id serial4 NOT NULL,
  customer_id int4 NOT NULL,
  order_date timestamp DEFAULT CURRENT_TIMESTAMP NULL,
  total_amount numeric(10, 2) NOT NULL,
  status varchar(50) DEFAULT 'pending'::character varying NOT NULL
);

-- Permissions

ALTER TABLE public.orders OWNER TO postgres;
GRANT ALL ON TABLE public.orders TO postgres;

-- public.products определение

-- Drop table

-- DROP TABLE public.products;

CREATE TABLE public.products (
  product_id serial4 NOT NULL,
  "name" varchar(100) NOT NULL,
  description text NULL,
  price numeric(10, 2) NOT NULL,
  image_url varchar(255) NULL,
  created_at timestamp DEFAULT CURRENT_TIMESTAMP NULL,
  updated_at timestamp DEFAULT CURRENT_TIMESTAMP NULL,
  CONSTRAINT products_pkey PRIMARY KEY (product_id)
);

-- Permissions

ALTER TABLE public.products OWNER TO postgres;
GRANT ALL ON TABLE public.products TO postgres;





