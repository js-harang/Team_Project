<?php
$con = mysqli_connect("localhost", "root", "1234", "sqlDB");

if($con)
	echo "connect : success<br>";
else
	echo "connect : failure<br>";

$sql = "select * from userTBL";

$ret = mysqli_query($con, $sql);

if($ret)
	echo mysqli_num_rows($ret), "건이 조회됨.<br><br>";
else
{
	echo "sqlDB 생성 실패!!<br>";
	echo "실패 원인 : ".mysqli_error($con);
}

while($row = mysqli_fetch_array($ret))
{
	//echo $row['userID'], " ", $row['name'], " ", $row['height'], " ", "<br>";
	echo $row[0], " ", $row[1], " ", $row[6], " ", "<br>";
}

mysqli_close($con);
?>