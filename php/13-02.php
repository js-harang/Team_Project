<?php>
$con = mysqli_connect("localhost", "root", "1234", "");
if($con)
	echo "connect : success<br>";
else
	echo "connect : failure<br>";

$sql = "create database sqlDB";
$ret = meysqli_query($con, $sql);

if($ret)
	echo "sqlDB가 성공적으로 생성됨.";
else
{
	echo "sqlDB 생성 실패!!<br>";
	echo "실패 원인 : ".mysqli_error($con);
}

mysqli_close($con);
?>