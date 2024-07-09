<?php
$con = mysqli_connect("localhost", "root", "1234", "sqlDB");
if($con)
	echo "connect : success<br>";
else
	echo "connect : failure<br>";

$sql = "create table userTbl
		(
			userID		char(8) not null primary key,
			name		varchar(10) not null,
			birthYear	int not null,
			addr		char(2) not null,
			mobile1		char(3),
			mobile2		char(8),
			height		smallint,
			mDate		date
		)";
$ret = mysqli_query($con, $sql);

if($ret)
	echo "sqlDB가 성공적으로 생성됨.";
else
{
	echo "sqlDB 생성 실패!!<br>";
	echo "실패 원인 : ".mysqli_error($con);
}

mysqli_close($con);
?>