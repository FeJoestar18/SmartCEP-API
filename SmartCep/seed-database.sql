-- =====================================================
-- Script para Popular Banco de Dados - SmartCEP
-- =====================================================

-- CEPs de São Paulo
INSERT INTO postal_codes (postal_code, street, neighborhood, city, state)
VALUES 
('01001000', 'Praça da Sé', 'Sé', 'São Paulo', 'SP'),
('01310100', 'Avenida Paulista', 'Bela Vista', 'São Paulo', 'SP'),
('04717000', 'Avenida Engenheiro Luís Carlos Berrini', 'Cidade Monções', 'São Paulo', 'SP'),
('05508000', 'Avenida Professor Francisco Morato', 'Butantã', 'São Paulo', 'SP');

-- CEPs do Rio de Janeiro
INSERT INTO postal_codes (postal_code, street, neighborhood, city, state)
VALUES 
('20040020', 'Rua da Assembleia', 'Centro', 'Rio de Janeiro', 'RJ'),
('22041001', 'Avenida Atlântica', 'Copacabana', 'Rio de Janeiro', 'RJ'),
('22421030', 'Avenida Vieira Souto', 'Ipanema', 'Rio de Janeiro', 'RJ');

-- CEPs de Brasília
INSERT INTO postal_codes (postal_code, street, neighborhood, city, state)
VALUES 
('70040902', 'Praça dos Três Poderes', 'Zona Cívico-Administrativa', 'Brasília', 'DF'),
('70297400', 'Esplanada dos Ministérios', 'Zona Cívico-Administrativa', 'Brasília', 'DF');

-- CEPs de Belo Horizonte
INSERT INTO postal_codes (postal_code, street, neighborhood, city, state)
VALUES 
('30140110', 'Avenida Afonso Pena', 'Centro', 'Belo Horizonte', 'MG'),
('30190922', 'Praça da Liberdade', 'Funcionários', 'Belo Horizonte', 'MG');

-- CEPs de Porto Alegre
INSERT INTO postal_codes (postal_code, street, neighborhood, city, state)
VALUES 
('90010270', 'Rua dos Andradas', 'Centro Histórico', 'Porto Alegre', 'RS'),
('90035000', 'Avenida Borges de Medeiros', 'Praia de Belas', 'Porto Alegre', 'RS');

SELECT COUNT(*) as total_ceps FROM postal_codes;

