<?php
$servername = "192.168.52.187";
$username = "root";
$password = "1234";
$dbname = "team_project";

$uid = $_POST["uid"];
$name = $_POST["name"];
$class = $_POST["class"];

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
    echo "Failed to connect to MySQL : " + $mysqli->connect_error;
}

if (search($conn, $name) === false) {
    $sql = "insert into character_list(uid, name, lv, class)
        values ('$uid', '$name', '1', '$class')";
    $result = mysqli_query($conn, $sql);
    
    if ($result)
        echo "success";
} else
    echo "name exists";

return;

function search($conn, $name)
{
    $sql = "select  name
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