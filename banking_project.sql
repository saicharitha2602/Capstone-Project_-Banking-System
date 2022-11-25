create database banking_project;
use banking_project;



create table user_accounts
(fullname varchar(30),email_id varchar(30) unique(email_id), phno bigint, acc_no bigint,acc_type varchar(30),balance bigint,
username varchar(20) primary key,password varchar(20));


create or alter procedure add_user_accounts(@fullname varchar(30), @email_id varchar(30),@phno bigint,@acc_no bigint,@acc_type varchar(30),@balance bigint,
@username varchar(20),@password varchar(20))
as
insert into user_accounts values(@fullname,@email_id,@phno,@acc_no,@acc_type,@balance,@username,@password);



create or alter procedure authenticate_user(@username varchar(20),@password varchar(20))
as
select * from user_accounts where username=@username and password=@password


--drop table transactions;
create table transactions(transaction_id bigint primary key,sender_accno bigint,receiver_accno bigint,amount bigint,tx_time smalldatetime)

create or alter procedure transaction_history(@transaction_id bigint,@sender_accno bigint,@receiver_accno bigint, @amount bigint,@tx_time smalldatetime)
as
insert into transactions values(@transaction_id,@sender_accno,@receiver_accno,@amount,@tx_time)
update user_accounts set balance=balance+@amount where acc_no=@receiver_accno;
update user_accounts set balance=balance-@amount where acc_no=@sender_accno;



select * from transactions;

exec transaction_history 456,8764368573,91837492748,100,'2022-11-17 11:11:11';

create or alter procedure sent_transactions(@acc_no bigint)
as
select *from transactions where sender_accno=@acc_no order by tx_time desc;



create or alter procedure received_transactions(@acc_no bigint)
as
select *from transactions where receiver_accno=@acc_no order by tx_time desc;

create or alter procedure view_balance(@acc_no bigint)
as 
select acc_no,acc_type,balance from user_accounts where acc_no=@acc_no


create table deposits(deposit_id bigint primary key,username varchar(20),fullname varchar(30),email_id varchar(30),phno bigint,deposit_type varchar(10),years int,amount bigint,status varchar(20));

create or alter procedure deposits_procedure(@deposit_id bigint,@username varchar(20),@fullname varchar(30),@email_id varchar(30),@phno bigint,@deposit_type varchar(10),@years int,@amount bigint,@status varchar(20))
as
insert into deposits values(@deposit_id,@username,@fullname,@email_id,@phno,@deposit_type,@years,@amount,@status)


create table loans(loan_id bigint primary key,username varchar(20),fullname varchar(30),email_id varchar(30),phno bigint,age varchar(10),annual_income bigint, loan_type varchar(10),loan_amount bigint,years int,status varchar(20));

drop table loans

create or alter procedure loans_procedure(@loan_id bigint,@username varchar(20),@fullname varchar(30),@email_id varchar(30),@phno bigint,@age varchar(10),@annual_income bigint, @loan_type varchar(10),@loan_amount bigint,@years int,@status varchar(20))
as
insert into loans values (@loan_id,@username,@fullname,@email_id,@phno,@age,@annual_income,@loan_type,@loan_amount,@years,@status)



