<?php
	$con = mysqli_connect("localhost", "root", "1234", "sqlDB");
	
	$userID = $_POST["userID"];
	$name = $_POST["name"];
	$birthYear = $_POST["birthYear"];
	$addr = $_POST["addr"];
	$mobile1 = $_POST["mobile1"];
	$mobile2 = $_POST["mobile2"];
	$height = $_POST["height"];
	$mDate = date("Y-m-j");
	$gender = $_POST["gender"];
	
	$sql = "insert into userTbl values('".$userID."','"
										 .$name."','"
										 .$birthYear."','"
										 .$addr."','"
										 .$mobile1."','"
										 .$mobile2."','"
										 .$height."','"
										 .$mDate."','"
										 .$gender."')";
										 
	$ret = mysqli_query($con, $sql);
	
	echo "<h1> 신규 회원 입력 결과 </h1>";
	if($ret)
	{
		echo "데이터가 성공적으로 입력됨.";
	}
	else
	{
		echo "데이터 입력 실패!!!<br>";
		echo "실패 원인 : ".mysqli_error($con);
	}
	
	mysqli_close($con);
	echo "<br> <a href='main.html'> <-- 초기화면 </a>";
?>