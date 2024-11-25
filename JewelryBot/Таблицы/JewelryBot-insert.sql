INSERT INTO public.products
(product_id, "name", description, price, image_url, created_at, updated_at)
VALUES(nextval('products_product_id_seq'::regclass), 'Кольцо', '', 500.00, 'https://i.pinimg.com/originals/ab/1d/07/ab1d071caa9fb4b8e4de50121008661c.jpg', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO public.products
(product_id, "name", description, price, image_url, created_at, updated_at)
VALUES(nextval('products_product_id_seq'::regclass), 'Серьги', '', 1000.00, 'https://i.pinimg.com/736x/60/f0/71/60f07112176bb62889670bdd15e87423.jpg', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);


INSERT INTO public.products
(product_id, "name", description, price, image_url, created_at, updated_at)
VALUES(nextval('products_product_id_seq'::regclass), 'Браслет', '', 750.00, 'https://i.pinimg.com/originals/68/09/e7/6809e7bb8c8ca15cdd8dcd2823c608a6.jpg', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
