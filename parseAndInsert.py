import json
import psycopg2

def cleanStr4SQL(s):
	return s.replace("'","`").replace("\n"," ")

def int2BoolStr (value):
	if value == 0:
		return 'False'
	else:
		return 'True'

def insert2BusinessTable():
	#reading the JSON file
	with open('./yelp_business.JSON','r') as f:	#TODO: update path for the input file
		#outfile =  open('./yelp_business.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
		line = f.readline()
		count_line = 0

		#connect to yelpdb database on postgres server using psycopg2
		#TODO: update the database name, username, and password
		try:
			conn = psycopg2.connect("dbname='milestone2' user='postgres' host='localhost' password='password'")
		except:
			print('Unable to connect to the database!')
		cur = conn.cursor()

		while line:
			data = json.loads(line)
			# Generate the INSERT statement for the cussent business
			# TODO: The below INSERT statement is based on a simple (and incomplete) businesstable schema. Update the statement based on your own table schema and
			# include values for all businessTable attributes
			sql_str = "INSERT INTO Business (business_id, name, address, b_state, city, zipcode, latitude, longitude, stars, review_count, num_checkins, review_rating, is_open) " \
					  "VALUES ('" + cleanStr4SQL(data['business_id']) + "','" + cleanStr4SQL(data["name"]) + "','" + cleanStr4SQL(data["address"]) + "','" + \
					  cleanStr4SQL(data["state"]) + "','" + cleanStr4SQL(data["city"]) + "','" + cleanStr4SQL(data["postal_code"]) + "'," + str(data["latitude"]) + "," + \
					  str(data["longitude"]) + "," + str(data["stars"]) + "," + str(data["review_count"]) + ",0 ,0.0,"  + int2BoolStr(data["is_open"]) + ");"
			try:
				cur.execute(sql_str)
			except psycopg2.Error as e:
				print("Insert to Business table failed!")
				print("Error message: ", e)
			conn.commit()
			# optionally you might write the INSERT statement to a file.
			# outfile.write(sql_str)

			line = f.readline()
			count_line +=1

		cur.close()
		conn.close()

	print(count_line)
	#outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
	f.close()

def insert2UserTable():
	#reading the JSON file
	with open('./yelp_user.JSON','r') as f:
		line = f.readline()
		count_line = 0

		try:
			conn = psycopg2.connect("dbname='milestone2' user='postgres' host='localhost' password='password'")
		except:
			print('Unable to connect to the database!')
		cur = conn.cursor()

		while line:
			data = json.loads(line)
			sql_str = "INSERT INTO Users (user_id, name, average_stars, fans, cool, review_count, funny, yelping_since, useful) " \
					  "VALUES ('" + cleanStr4SQL(data['user_id']) + "','" + cleanStr4SQL(data["name"]) + "'," + str(data["average_stars"]) + "," + \
					  str(data["fans"]) + "," + str(data["cool"]) + "," + str(data["review_count"]) + "," + str(data["funny"]) + ",'" + \
					  cleanStr4SQL(data["yelping_since"]) + "'," + str(data["useful"]) + ");"
			try:
				cur.execute(sql_str)
			except psycopg2.Error as e:
				print("Insert to User table failed!")
				print("Error message: ", e)
			conn.commit()

			line = f.readline()
			count_line +=1

		cur.close()
		conn.close()

	print(count_line)
	f.close()

def insert2ReviewTable():
	#reading the JSON file
	with open('./yelp_review.JSON','r') as f:
		line = f.readline()
		count_line = 0

		try:
			conn = psycopg2.connect("dbname='milestone2' user='postgres' host='localhost' password='password'")
		except:
			print('Unable to connect to the database!')
		cur = conn.cursor()

		while line:
			data = json.loads(line)
			sql_str = "INSERT INTO Reviews (review_id, review_stars, review_date, text, useful_vote, funny_vote, cool_vote, user_id, business_id) " \
					  "VALUES ('" + cleanStr4SQL(data['review_id']) + "'," + str(data["stars"]) + ",'" + cleanStr4SQL(data["date"]) + "','" + \
					  cleanStr4SQL(data["text"]) + "'," + str(data["useful"]) + "," + str(data["funny"]) + "," + str(data["cool"]) + ",'" + \
					  cleanStr4SQL(data["user_id"]) + "','" + cleanStr4SQL(data["business_id"]) + "');"
			try:
				cur.execute(sql_str)
			except psycopg2.Error as e:
				print("Insert to Reviews table failed!")
				print("Error message: ", e)
			conn.commit()

			line = f.readline()
			count_line +=1

		cur.close()
		conn.close()

	print(count_line)
	f.close()

def insert2CategoriesTable():
	#reading the JSON file
	with open('./yelp_business.JSON','r') as f:
		line = f.readline()
		count_line = 0

		try:
			conn = psycopg2.connect("dbname='milestone2' user='postgres' host='localhost' password='password'")
		except:
			print('Unable to connect to the database!')
		cur = conn.cursor()

		while line:
			data = json.loads(line)
			for category in data["categories"]:
				sql_str = "INSERT INTO Categories (category_name, business_id) " \
					"VALUES ('" + cleanStr4SQL(category) + "','" + cleanStr4SQL(data["business_id"]) + "');"
				try:
					cur.execute(sql_str)
				except psycopg2.Error as e:
					print("Insert to Categories table failed!")
					print("Error message: ", e)
				conn.commit()

			line = f.readline()
			count_line +=1

		cur.close()
		conn.close()

	print(count_line)
	f.close()

def insert2HoursTable():
	#reading the JSON file
	with open('./yelp_business.JSON','r') as f:
		line = f.readline()
		count_line = 0

		try:
			conn = psycopg2.connect("dbname='milestone2' user='postgres' host='localhost' password='password'")
		except:
			print('Unable to connect to the database!')
		cur = conn.cursor()

		while line:
			data = json.loads(line)
			days = data["hours"]
			for day in days:
				open_close = days[day].split('-')
				sql_str = "INSERT INTO Hours (h_open, h_close, h_day, business_id) " \
					"VALUES ('" + cleanStr4SQL(open_close[0]) + "','" + cleanStr4SQL(open_close[1]) + "','" + \
					cleanStr4SQL(day) + "','" + cleanStr4SQL(data["business_id"]) + "');"
				try:
					cur.execute(sql_str)
				except psycopg2.Error as e:
					print("Insert to Hours table failed!")
					print("Error message: ", e)
				conn.commit()

			line = f.readline()
			count_line +=1

		cur.close()
		conn.close()

	print(count_line)
	f.close()

def insert2CheckinsTable():
	#reading the JSON file
	with open('./yelp_checkin.JSON','r') as f:
		line = f.readline()
		count_line = 0

		try:
			conn = psycopg2.connect("dbname='milestone2' user='postgres' host='localhost' password='password'")
		except:
			print('Unable to connect to the database!')
		cur = conn.cursor()

		while line:
			data = json.loads(line)
			checkins = data["time"]
			for day in checkins:
				for hour in checkins[day]:
					sql_str = "INSERT INTO Checkins (c_count, c_day, c_time, business_id) " \
						"VALUES ('" + str(checkins[day][hour]) + "','" + cleanStr4SQL(day) + "','" + \
						cleanStr4SQL(hour) + "','" + cleanStr4SQL(data["business_id"]) + "');"
					try:
						cur.execute(sql_str)
					except psycopg2.Error as e:
						print("Insert to Checkins table failed!")
						print("Error message: ", e)
					conn.commit()

			line = f.readline()
			count_line +=1

		cur.close()
		conn.close()

	print(count_line)
	f.close()

def insert2FriendsTable():
	#reading the JSON file
	with open('./yelp_user.JSON','r') as f:
		line = f.readline()
		count_line = 0
		retired = {} 
		
		try:
			conn = psycopg2.connect("dbname='milestone2' user='postgres' host='localhost' password='password'")
		except:
			print('Unable to connect to the database!')
		cur = conn.cursor()

		while line:
			data = json.loads(line)
			retired[data["user_id"]] = 0
			for friend in data["friends"]:
				if friend not in retired:
					sql_str = "INSERT INTO Friends (user1, user2) " \
						"VALUES ('" + cleanStr4SQL(data['user_id']) + "','" + cleanStr4SQL(friend) + "');"
					try:
						cur.execute(sql_str)
					except psycopg2.Error as e:
						print("Insert to Friends table failed!")
						print("Error message: ", e)
					conn.commit()

			line = f.readline()
			count_line +=1

		cur.close()
		conn.close()

	print(count_line)
	f.close()

print("Inserting business table...")
insert2BusinessTable()
print("Done with business table.\nInserting User table...")
insert2UserTable()
print("Done with User table.\nInserting Review table...")
insert2ReviewTable()
print("Done with Review table.\nInserting Category table...")
insert2CategoriesTable()
print("Done with Category table.\nInserting Hour table...")
insert2HoursTable()
print("Done with Hour table.\nInserting Checkin table...")
insert2CheckinsTable()
print("Done with Checkin table.\nInserting Friends table...")
insert2FriendsTable()
print("Done with Friends table.")