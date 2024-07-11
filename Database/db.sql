drop database if exists db_event;
create database db_event;

use db_event;

create table account(
    id  int primary key auto_increment,
    name varchar(256),
    username varchar(100),
    password varchar(100)
);

/*
insert into account(name, username, password) values
("name1", "user1", "12345"),
("name2", "user2", "12345"),
("name3", "user3", "12345");

select id, name, username, password from account;

insert into account(name, username, password) value ('name4', 'user4', '12345');
insert into account(name, username, password) value ('test11', 'test11', 'test11');
*/

-- event
--use db_event;
create table event(
    id          int primary key auto_increment,
    name        varchar(256),
    time        DateTime,
    address     varchar(256),
    description varchar(256)
);
/*

select id, name, time, address, description from event;

insert into event(name, address) values
("event01", "address01"),
("event02", "address02"),
("event03", "address03"),
("event04", "address04"),
("event05", "address05"),
("event06", "address06"),
("event07", "address07"),
("event08", "address08"),
("event09", "address09");
*/
-- request table
create table request (
    id int primary key auto_increment,
    eventId int,
    ownerId int,
    userId int,
    state int,
    createAt DateTime,
    constraint foreign key (eventId) references event(id),
    constraint foreign key (ownerId) references account(id),
    constraint foreign key (userId) references account(id)
);


-- invite table
create table invite (
    id int primary key auto_increment,
    eventId int,
    ownerId int,
    userId int,
    state int,
    createAt DateTime,
    constraint foreign key (eventId) references event(id),
    constraint foreign key (ownerId) references account(id),
    constraint foreign key (userId) references account(id)
);
/*

select id, createAt, state, eventId, ownerId, userId from invite;

use db_event;
*/
create table account_event(
    id int primary key auto_increment,
    ownerId int,
    eventId int,
    constraint foreign key (ownerId) references account(id),
    constraint foreign key (eventId) references event(id)
);

/*
select max(id) from event;

insert into account_event(eventId, ownerId) value (@eventId, @ownerId)

insert into request(eventId, ownerId, userId, createAt) " +
                "value(@eventId, @ownerId, @userId, @createAt);

use db_event;
insert into request(eventId, ownerId, userId, state, createAt) 
value(1,1,2,0, "2000-12-12 12:12:00"); 

select event.id, name, time, address, description from event 
inner join account_event on event.id = account_event.eventId 
where account_event.ownerId = 1;

use db_event;

*/
create table member(
    id  int primary key auto_increment,
    eventId int,
    memberId int,
    constraint foreign key (eventId) references event(id),
    constraint foreign key (memberId) references account(id)
);

/*

select id, eventId, memberId from member;

insert into member(eventId, memberId) values
(17, 1),
(17, 2),
(17, 3),
(17, 4);


insert into invite(eventId, ownerId, userId, createAt) value(, @ownerId, @userId, @createAt);

use db_event;

update invite set state = 1 where eventId = 16 and 
ownerId = 1 and userId = 2;

use db_event;

*/

/*
set @state = 1;
set @eventId = 1;
set @ownerId = 1;
set @userId = 3;
update invite set state = 11 where eventId = 1 and 
ownerId = 1 and userId = 3;

*/