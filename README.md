# WebAppTestDOC
Signing manager

Тестовое задание
Общее описание системы
Тестовое задание представляет из себя упрощённую версию сервиса подписания документов через СМС. Компания, используя свои учетные данные загружает на сервер документ в формате PDF. Вместе с документом должны отправляться реквизиты второй стороны так как:
•	Номер телефона
•	ИИН/БИН
•	ФИО
После отправки документа формируется URL. Вторая сторона проходит по указанному URL должна увидеть документ на подписание. После успешного подписания система должна отредактировать итоговый документ путем вставки QR-кодов с реквизитами второй стороны. После чего ту же самую процедуру должна провести компания отправившая документ. После чего документ считается подписанный всеми сторонами. 
p/s Реализовывать Front нет необходимости. Использовать только API