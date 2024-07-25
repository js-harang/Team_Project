<?php
$servername = "192.168.52.187";
$username = "root";
$password = "1234";
$dbname = "team_project";

$uid = $_POST["uid"];
$slot = $_POST["slot"];
$name = $_POST["name"];
$class = $_POST["class"];

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
    echo "Failed to connect to MySQL : " + $mysqli->connect_error;
}

if (search($conn, $name) === false) {
    $sql = "INSERT into character_list(uid, slot, name, class)
            values ('$uid', '$slot', '$name', '$class')";
    $result = mysqli_query($conn, $sql);

    if ($result)
        echo "success";
} else
    echo "name exists";

mysqli_close($conn);

function search($conn, $name)
{
    $sql = "SELECT  name
            from    character_list
            where   name = '$name'";
    $result = mysqli_query($conn, $sql);
    $count = mysqli_num_rows($result);

    if ($count == 0)
        return false;
    else
        return true;
}
?>