var mainApp = angular.module('mainModule', []);

mainApp.controller('MainPageController',
    function ($scope, $http) {

        //$http({ method: 'GET', url: "api/GraphDataApi/GetExampleGraph" }).
        //    then(function success(response) {
        //    var nodes = [];
        //    var edges = [];
        //    for (var i = 0; i < model.length; i++) {
        //        nodes.push({
        //            id: model[i].Parent.Index,
        //            label: model[i].Parent.Index
        //        });
        //        edges.push({
        //            from: model[i].Parent.Index,
        //            to: model[i].Child.Index
        //        });
        //    }
        //    nodes.push({
        //        id: model[model.length - 1].Child.Index,
        //        label: model[model.length - 1].Child.Index
        //    });
        //    var visnodes = new vis.DataSet(nodes);
        //    var visedges = new vis.DataSet(edges);

        //    var container = document.getElementById('mynetwork');
        //    var data = {
        //        nodes: visnodes,
        //        edges: visedges
        //    };
        //    var options = {
        //        layout: {
        //            randomSeed: undefined,
        //            improvedLayout: true,
        //            hierarchical: {
        //                enabled: true,
        //                levelSeparation: 80,
        //                nodeSpacing: 1,
        //                treeSpacing: 10,
        //                blockShifting: true,
        //                edgeMinimization: false,
        //                parentCentralization: false,
        //                direction: 'UD',        // UD, DU, LR, RL
        //                sortMethod: 'directed'  // hubsize, directed
        //            }
        //        }
        //    };
        //    new vis.Network(container, data, options);
        //});
        //});
        var i,
       s,
       N = 100,
       E = 500,
       g = {
           nodes: [],
           edges: []
       };
        var nodes = [];
        var edges = [];

        g.nodes.push({
            id: model[model.length - 1].Child.Index.toString(),
            label: model[model.length - 1].Child.Index,
            x: Math.random(),
            y: Math.random(),
            size: Math.random(),
            color: '#666'
        });
        for (var i = 0; i < model.length; i++) {
            g.nodes.push({
                id: model[i].Parent.Index.toString(),
                label: 'Node ' + i,
                x: Math.random(),
                y: Math.random(),
                size: 10,
                color: '#666'
            });

            g.edges.push({
                id: 'e' + i,
                source: model[i].Parent.Index.toString(),
                target: model[i].Child.Index.toString(),
                size: 5,
                color: '#ccc'
            });
        }


        // Instantiate sigma:
        s = new sigma({
            graph: g,
            container: 'graph-container'
        });
    });
mainApp.controller("runExtensionController",
    function ($scope, $http) {
        $scope.fileNameChanged = function (ele) {
            var file = ele.files[0];
            $http({
                method: 'POST',
                data: file,
                url: "/api/GraphsApi/GetCpf"
            })
                .then(function success(response) {
                    alert("YES!");
                });
        }
    });

mainApp.controller("parallelFormController",
function () {
    var i,
       s,
       N = 100,
       E = 500,
       g = {
           nodes: [],
           edges: []
       };
    var nodes = [];
    var edges = [];

    g.nodes.push({
        id: model.NodesConnections[model.NodesConnections.length - 1].Child.Index.toString(),
        label: model.NodesConnections[model.NodesConnections.length - 1].Child.Index,
        x: 10,
        y: 50,
        size: 10,
        color: '#666'
    });

    var levelsLengh = [];
    for(var i = 0; i < model.ParallelForm.length; i++)
    {
        levelsLengh.push(0);
    }

    for (var i = 0; i < model.NodesConnections.length; i++) {
        var level = GetLevel(model.NodesConnections[i].Parent.Index);
        g.nodes.push({
            id: model.NodesConnections[i].Parent.Index.toString(),
            label: 'Node ' + i,
            y: level*10,
            x: levelsLengh[level]*10,
            size: 10,
            color: '#666'
        });
        levelsLengh[level]++;
        g.edges.push({
            id: 'e' + i,
            source: model.NodesConnections[i].Parent.Index.toString(),
            target: model.NodesConnections[i].Child.Index.toString(),
            size: 5,
            color: '#ccc'
        });
    }

    // Instantiate sigma:
    s = new sigma({
        graph: g,
        container: 'graph-container'
    });

    function  GetLevel(index)
    {
        var levels = model.ParallelForm;
        
        for (var i = 0; i < levels.length; i++)
        { 
            for (var j = 0; j < levels[i].length; j++)
            {
                if (levels[i][j] == index)
                    return i;
            }
        }
        return -1;
    }
});

mainApp.controller("lineralFormController",
function () {
    var i,
       s,
       N = 100,
       E = 500,
       g = {
           nodes: [],
           edges: []
       };
    var nodes = [];
    var edges = [];

    g.nodes.push({
        id: model.NodesConnections[model.NodesConnections.length - 1].Child.Index.toString(),
        label: model.NodesConnections[model.NodesConnections.length - 1].Child.Index,
        x: 10,
        y: 50,
        size: 10,
        color: '#666'
    });

    var levelsLengh = [];
    for (var i = 0; i < model.ParallelForm.length; i++) {
        levelsLengh.push(0);
    }

    for (var i = 0; i < model.NodesConnections.length; i++) {
        var level = GetLevel(model.NodesConnections[i].Parent.Index);
        g.nodes.push({
            id: model.NodesConnections[i].Parent.Index.toString(),
            label: 'Node ' + i,
            y: level * 10,
            x: levelsLengh[level] * 10,
            size: 10,
            color: '#666'
        });
        levelsLengh[level]++;
        g.edges.push({
            id: 'e' + i,
            source: model.NodesConnections[i].Parent.Index.toString(),
            target: model.NodesConnections[i].Child.Index.toString(),
            size: 5,
            color: '#ccc'
        });
    }

    // Instantiate sigma:
    s = new sigma({
        graph: g,
        container: 'lineral-graph-container'
    });

    function GetLevel(index) {
        var levels = model.ParallelForm;

        for (var i = 0; i < levels.length; i++) {
            for (var j = 0; j < levels[i].length; j++) {
                if (levels[i][j] == index)
                    return i;
            }
        }
        return -1;
    }
});
