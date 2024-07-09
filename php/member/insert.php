<html>
	<head>
		<meta http-equiv="content-type" content="text/html; charset=utf-8">
	</head>
	<body>
		<h1> 신규 회원 입력 </h1>
		<form method="post" action="insert_result.php">
			아이디 : <input type="text" name="userID"><br>
			이름 : <input type="text" name="name"><br>
			출생년도 : <input type="text" name="birthYear"><br>
			지역 : <input type="text" name="addr"><br>
			휴대폰 국번 : <input type="text" name="mobile1"><br>
			휴대폰 전화번호 : <input type="text" name="mobile2"><br>
			신장 : <input type="text" name="height"><br>
			성별 : <input type = "radio" name = "gender" value = "남성"> 남성
				  <input type = "radio" name = "gender" value = "여성"> 여성
			<br><br>
			<input type="submit" value="회원 입력">
		</form>
	</body>
</html>