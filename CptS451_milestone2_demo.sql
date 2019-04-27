--glossary
businesstable
usertable
reviewtable
friendstable
checkintable
businesscategory
businessattribute
businesshours
zipcode
business_id
city  (business city)
name   (business name)
user_id
friend_id
review_id
reviewcount
reviewtext
reviewstars
reviewdate
reviewrating
numCheckins
checkinday  
checkintime 
checkincount 


--1.
SELECT COUNT(*) 
FROM  businesstable;
SELECT COUNT(*) 
FROM  usertable;
SELECT COUNT(*) 
FROM  reviewtable;
SELECT COUNT(*) 
FROM  friendstable;
SELECT COUNT(*) 
FROM  checkintable;
SELECT COUNT(*) 
FROM  businesscategory;
SELECT COUNT(*) 
FROM  businessattribute;
SELECT COUNT(*) 
FROM  businesshours;



--2. Run the following queries on your business table, checkin table and review table. Make sure to change the attribute names based on your schema. 

SELECT zipcode, count(business_id) 
FROM businesstable
GROUP BY zipcode
HAVING count(business_id)>400
ORDER BY zipcode

SELECT zipcode, COUNT(distinct C.category)
FROM businesstable as B, businesscategory as C
WHERE B.business_id = C.business_id
GROUP BY zipcode
HAVING count(distinct C.category)>75
ORDER BY zipcode

SELECT zipcode, COUNT(distinct A.attribute)
FROM businesstable as B, businessattribute as A
WHERE B.business_id = A.business_id
GROUP BY zipcode
HAVING count(distinct A.attribute)>80


--3. Run the following queries on your business table, checkin table and tips table. Make sure to change the attribute names based on your schema. 

SELECT usertable.user_id, count(friend_id)
FROM usertable, friendstable
WHERE usertable.user_id = friendstable.user_id AND 
      usertable.user_id = 'zvbewosyFz94fSlmoxTdPQ'
GROUP BY usertable.user_id

SELECT business_id, name, city, reviewcount, numCheckins, reviewrating 
FROM businesstable 
WHERE business_id ='6lovZEiwWcRYRhyKd94DRg' ;

-----------

SELECT SUM(checkincount) 
FROM checkintable 
WHERE business_id ='6lovZEiwWcRYRhyKd94DRg';

SELECT count(*), avg(reviewstars)
FROM reviewtable
WHERE  business_id = '6lovZEiwWcRYRhyKd94DRg';


--4. 
--Type the following statements. Make sure to change the attribute names based on your schema.  Don’t run the update statement before you show the results for steps 1 and 2 to the TA.

UPDATE checkintable 
SET checkincount = checkincount + 1 
WHERE business_id ='6lovZEiwWcRYRhyKd94DRg'  AND checkinday ='Friday' AND checkintime = '15:00';

INSERT INTO checkintable (business_id, checkinday,checkintime,checkincount)
VALUES ('h_vsOvGHQtEpUroh-5lcHA','Friday','15:00',1);

------------


SELECT business_id,name, city, numCheckins 
FROM businesstable 
WHERE business_id ='h_vsOvGHQtEpUroh-5lcHA';



--5.
--Type the following statements. Make sure to change the attribute names based on your schema.  Don’t run the insert statement before you show the results for the first query to the TA.

INSERT INTO reviewtable (review_id, user_id, business_id,reviewtext,reviewstars,reviewdate,funny,useful,cool)  
VALUES ('ZuRjoWuinqWhecT-PRZ-qw','zvbewosyFz94fSlmoxTdPQ', '6lovZEiwWcRYRhyKd94DRg', 'Great!',5,'2019-03-22',0,0,0);



