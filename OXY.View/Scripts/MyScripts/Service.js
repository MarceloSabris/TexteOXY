app.service( 'VeiculoService' , function ($http) {
    //Create new record
    this .post = function (Student) {
        var request = $http({
            method: "post" ,
            url: "/api/Carro" ,
            data: Student
        });
        return request;
    }
    //Get Single Records
    this .get = function (Id) {
        return $http.get( "/api/Veiculo/" + Id);
    }
    //Get All Students
    this .getStudents = function () {
        return $http.get( "/api/Veiculo" );
    }
    //Update the Record
    this .put = function (Id, Veiculo) {
        var request = $http({
            method: "put" ,
            url: "/api/Veiculo/" + Id,
            data: Veiculo
        });
        return Veiculo;
    }
    //Delete the Record
    this .delete = function (Id) {
        var request = $http({
            method: "delete" ,
            url: "/api/Veiculo/" + Id
        });
        return request;
    }
});

controller( 'VeiculoController' , function ($scope, VeiculoService) {
    $scope.IsNewRecord = 1; //The flag for the new record
    loadRecords();
    //Function to load all Student records
    function loadRecords() {
        var promiseGet = StudentService.getStudents(); //The MEthod Call from service
        promiseGet.then( function (pl) { $scope.Veiculos = pl.data },
        function (errorPl) {
            $log.error( 'Erro ao carregar veiculo Veiculo' , errorPl);
        });
    }
    //The Save scope method use to define the Student object.
    //In this method if IsNewRecord is not zero then Update Student else
    //Create the Student information to the server
    $scope.save = function () {
        var Veiculo = {
            Id: $scope.Id,
            Placa: $scope.Placa,
            Renavan: $scope.Renavan,
            NomeProprietario: $scope.NomeProprietario,
            CPF: $scope.CPF,
            FotosVeiculos: $scope.FotosVeiculos
        };
        //If the flag is 1 the it si new record
        if ($scope.IsNewRecord === 1) {
            var promisePost = VeiculoService.post(Veiculo);
            promisePost.then( function (pl) {
                $scope.Id = pl.data.Id;
                loadRecords();
            }, function (err) {
                console.log( "Err" + err);
            });
        } else { //Else Edit the record
            var promisePut = VeiculoService.put($scope.Id, Veiculo);
            promisePut.then( function (pl) {
                $scope.Message = "Updated Successfully" ;
                loadRecords();
            }, function (err) {
                console.log( "Err" + err);
            });
        }
    };
    //Method to Delete
    $scope.delete = function () {
        var promiseDelete = VeiculoService.delete($scope.Id);
        promiseDelete.then( function (pl) {
            $scope.Id='',
            $scope.Placa='',
            $scope.Renavan='',
            $scope.NomeProprietario='',
            $scope.CPF='',
            $scope.FotosVeiculos=''
            loadRecords();
        }, function (err) {
            console.log( "Err" + err);
        });
    }
    //Method to Get Single Student based on Id
    $scope.get = function (Veiculo) {
        var promiseGetSingle = VeiculoService.get(Veiculo.Id);
        promiseGetSingle.then( function (pl) {
            var res = pl.data;
            $scope.Id = res.Id;
            $scope.Placa = res.Placa;
            $scope.Renavan = res.Renavan;
            $scope.NomeProprietario = res.NomeProprietario;
            $scope.CPF = res.CPF;
            $scope.FotosVeiculos = res.FotosVeiculos;
            $scope.IsNewRecord = 0;
        },
        function (errorPl) {
            console.log( 'failure loading Student' , errorPl);
        });
    }
    //Clear the Scopr models
    $scope.clear = function () {
        $scope.Id = '';
        $scope.Placa ='';
        $scope.Renavan = '';
        $scope.NomeProprietario ='';
        $scope.CPF = '';
        $scope.FotosVeiculos = '';
        $scope.IsNewRecord = 0;
    }
});