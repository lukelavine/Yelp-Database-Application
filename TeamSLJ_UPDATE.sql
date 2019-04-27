UPDATE Business
SET review_count = (
	SELECT COUNT(*)
	FROM Business as b, Reviews as r
	WHERE b.business_id = r.business_id)
FROM Business as b, Reviews as r
WHERE b.business_id = r.business_id;

UPDATE Business
SET num_checkins = (
	SELECT SUM(c_count)
	FROM Business as b, Checkins as c
	WHERE b.business_id = c.business_id)
FROM Business as b, Checkins as c
WHERE b.business_id = c.business_id;

UPDATE Business
SET review_rating = (
	SELECT AVG(review_stars)
	FROM Business as b, Reviews as r
	WHERE b.business_id = r.business_id)
FROM Business as b, Reviews as r
WHERE b.business_id = r.business_id;