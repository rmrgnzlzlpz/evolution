var mapView;
var deptDialog;
var veredas;
var global = {};
var ver;
var prevLayer;
var graphicsLayer;

require([
  "esri/Map",
  "esri/views/MapView",
  "esri/layers/FeatureLayer",
  "esri/geometry/Point",
  "esri/widgets/Home",
  "esri/layers/GraphicsLayer",
  "esri/widgets/Sketch",
  "esri/widgets/Sketch/SketchViewModel",
  "dijit/form/Button",
  "dijit/Dialog",
  "dojo/domReady!"
],
  function (
    Map,
    MapView,
    FeatureLayer,
    Point,
    Home,
    GraphicsLayer,
    Sketch,
    SketchViewModel,
    Button,
    Dialog
  ) {
    var legend;

    var map = new Map({
      basemap: "topo-vector"
    });

    mapView = new MapView({
      container: "viewDiv",
      map: map,
      zoom: 6,
      center: [-72.4782449846235, 4.887407292289377]
    });

    graphicsLayer = new GraphicsLayer();

    map.add(graphicsLayer);

    var sketchVM = new SketchViewModel({
      layer: graphicsLayer,
      view: mapView,
      polygonSymbol: {
        type: "simple-fill",
        style: "none",
        outline: {
          color: "black",
          width: 1
        }
      }
    });

    function proper(feature) {
      console.log(feature.keys);
    }

    sketchVM.on(["create"], function (event) {
      if (event.state === "complete") {
        mapView.graphics.removeAll();
        for (var i = 0; i < map.layers.length; i++) {
          if (map.layers.items[i].type == "graphics") {
            map.remove(map.layers.items[i]);
          }
        }
        if (_depColLayer) {
          _depColLayer.opacity = 0;
          let featureLayerView = global.getFeatureLayerView();
          featureLayerView.filter = {
            geometry: event.graphic.geometry,
            spatialRelationship: event.tool == "point" ? "intersects" : "contains"
          }
          featureLayerView.queryFeatures().then((results) => {
            if (results.features.length == 1) {
              _depColLayer.renderer = justOutLineRenderer;
              mapView.popup.title = results.features[0].attributes.DPTO_CNMBRE;
              mapView.popup.open({
                location: {
                  latitude: results.features[0].geometry.centroid.latitude,
                  longitude: results.features[0].geometry.centroid.longitude
                },
                title: "Información de " + results.features[0].attributes.DPTO_CNMBRE,
                content: `
                  OBJECTID: ${results.features[0].attributes.OBJECTID} <br> 
                  Código DANE departamento: ${results.features[0].attributes.DPTO_CCDGO} <br> 
                  Año de creación del departamento: ${results.features[0].attributes.DPTO_NANO_CREACION} <br> 
                  Nombre del departamento: ${results.features[0].attributes.DPTO_CNMBRE} <br> 
                  Acto administrativo de creación del departamento: ${results.features[0].attributes.DPTO_CACTO_ADMNSTRTVO} <br> 
                  Área oficial del departamento en Km2: ${results.features[0].attributes.DPTO_NAREA} <br> 
                  Año vigencia de información municipal (Fuente DANE): ${results.features[0].attributes.DPTO_NANO} <br> 
                `
              });
              verColLayer.definitionExpression = `COD_DPTO=${results.features[0].attributes.DPTO_CCDGO}`;
              verColLayer.opacity = .75;
              mapView.goTo(results.features[0].geometry.extent.expand(1));
            }
          });
          // Se demora en cargar de nuevo
          /*
          const query = {
              returnGeometry: true,
              geometry: event.graphic.geometry,
              outFields: ["*"]
          };
          depColLayer.queryFeatures(query).then(function (results) {
              const graphics = results.features;
              if (graphics.length > 0) {
                  mapView.goTo(event.graphic.geometry.extent.expand(1));
              }
          });
          */
        }
      }
    });

    var sketch = new Sketch({
      view: mapView,
      viewModel: sketchVM,
      layer: graphicsLayer,
      creationMode: "single"
    });

    mapView.ui.add(sketch, "top-right");

    var homeBtn = new Home({
      view: mapView
    });

    var nomColLabel = {
      symbol: {
        type: "text",
        color: "white",
        haloColor: "black",
        haloSize: "1px",
        font: {
          size: "12px",
          family: "Noto Sans",
          style: "italic",
          weight: "normal"
        }
      },
      labelPlacement: "above-center",
      labelExpressionInfo: {
        expression: "$feature.DPTO_CNMBRE"
      }
    };

    var renderer = {
      type: "simple",
      symbol: {
        type: "simple-fill",
        color: "cyan",
        style: "solid",
        outline: {
          color: "black",
          width: 1
        }
      }
    };

    var justOutLineRenderer = {
      type: "simple",
      symbol: {
        type: "simple-line",
        color: "black",
        width: 2
      }
    };

    var verPopup = {
      "title": "Información de {NOMBRE_VER}",
      "content": [
        {
          "type": "fields",
          "fieldInfos": [
            {
              "fieldName": "OBJECTID",
              "label": "Id",
              "isEditable": true,
              "tooltip": "",
              "visible": true,
              "format": null,
              "stringFieldOption": "text-box"
            },
            {
              "fieldName": "DPTOMPIO",
              "label": "DPTOMPIO",
              "isEditable": true,
              "tooltip": "",
              "visible": true,
              "format": null,
              "stringFieldOption": "text-box"
            },
            {
              "fieldName": "CODIGO_VER",
              "label": "CODIGO_VER",
              "isEditable": true,
              "tooltip": "",
              "visible": true,
              "format": null,
              "stringFieldOption": "text-box"
            },
            {
              "fieldName": "NOM_DEP",
              "label": "NOM_DEP",
              "isEditable": true,
              "tooltip": "",
              "visible": true,
              "format": null,
              "stringFieldOption": "text-box"
            },
            {
              "fieldName": "NOMB_MPIO",
              "label": "NOMB_MPIO",
              "isEditable": true,
              "tooltip": "",
              "visible": true,
              "format": null,
              "stringFieldOption": "text-box"
            },
            {
              "fieldName": "NOMBRE_VER",
              "label": "NOMBRE_VER ",
              "isEditable": true,
              "tooltip": "",
              "visible": true,
              "format": null,
              "stringFieldOption": "text-box"
            },
            {
              "fieldName": "VIGENCIA",
              "label": "VIGENCIA",
              "isEditable": true,
              "tooltip": "",
              "visible": true,
              "format": null,
              "stringFieldOption": "text-box"
            },
            {
              "fieldName": "FUENTE",
              "label": "FUENTE",
              "isEditable": true,
              "tooltip": "",
              "visible": true,
              "format": null,
              "stringFieldOption": "text-box"
            },
            {
              "fieldName": "DESCRIPCIO",
              "label": "DESCRIPCIO",
              "isEditable": true,
              "tooltip": "",
              "visible": true,
              "format": null,
              "stringFieldOption": "text-box"
            },
            {
              "fieldName": "SEUDONIMOS",
              "label": "SEUDONIMOS",
              "isEditable": true,
              "tooltip": "",
              "visible": true,
              "format": null,
              "stringFieldOption": "text-box"
            },
            {
              "fieldName": "AREA_HA",
              "label": "AREA_HA",
              "isEditable": true,
              "tooltip": "",
              "visible": true,
              "format": null,
              "stringFieldOption": "text-box"
            },
            {
              "fieldName": "COD_DPTO",
              "label": "COD_DPTO",
              "isEditable": true,
              "tooltip": "",
              "visible": true,
              "format": null,
              "stringFieldOption": "text-box"
            }
          ]
        }]
    }

    var deptPopup = {
      "title": "Información de {DPTO_CNMBRE}",
      "content": [
        {
          "type": "fields",
          "fieldInfos": [
            {
              "fieldName": "OBJECTID",
              "label": "OBJECTID ",
              "isEditable": true,
              "tooltip": "",
              "visible": true,
              "format": null,
              "stringFieldOption": "text-box"
            },
            {
              "fieldName": "DPTO_CCDGO",
              "label": "Código DANE departamento",
              "isEditable": true,
              "tooltip": "",
              "visible": true,
              "format": null,
              "stringFieldOption": "text-box"
            },
            {
              "fieldName": "DPTO_NANO_CREACION",
              "label": "Año de creación del departamento",
              "isEditable": true,
              "tooltip": "",
              "visible": true,
              "format": null,
              "stringFieldOption": "text-box"
            },
            {
              "fieldName": "DPTO_CNMBRE",
              "label": "Nombre del departamento",
              "isEditable": true,
              "tooltip": "",
              "visible": true,
              "format": null,
              "stringFieldOption": "text-box"
            },
            {
              "fieldName": "DPTO_CACTO_ADMNSTRTVO",
              "label": "Acto administrativo de creación del departamento",
              "isEditable": true,
              "tooltip": "",
              "visible": true,
              "format": null,
              "stringFieldOption": "text-box"
            },
            {
              "fieldName": "DPTO_NAREA",
              "label": "Área oficial del departamento en Km2",
              "isEditable": true,
              "tooltip": "",
              "visible": true,
              "format": null,
              "stringFieldOption": "text-box"
            },
            {
              "fieldName": "DPTO_NANO",
              "label": "Año vigencia de información municipal (Fuente DANE)",
              "isEditable": true,
              "tooltip": "",
              "visible": true,
              "format": null,
              "stringFieldOption": "text-box"
            }
          ]
        }]
    }


    var depColLayer = new FeatureLayer({
      url: "https://ags.esri.co/server/rest/services/DA_DANE/departamento_mgn2016/MapServer",
      outFields: ["*"],
      opacity: .3,
      renderer: {
        type: "simple",
        symbol: {
          type: "simple-fill",
          color: "green",
          style: "solid",
          outline: {
            color: "black",
            width: 1
          }
        }
      },
      labelingInfo: [nomColLabel]
    });

    var _depColLayer = new FeatureLayer({
      url: "https://ags.esri.co/server/rest/services/DA_DANE/departamento_mgn2016/MapServer",
      outFields: ["*"],
      opacity: 0,
      renderer: renderer
    });

    var verColLayer = new FeatureLayer({
      url: "https://ags.esri.co/server/rest/services/DA_DatosAbiertos/VeredasColombia/MapServer/0",
      outFields: ["*"],
      opacity: 0,
      renderer: renderer,
      popupTemplate: verPopup
    });

    map.add(depColLayer);
    map.add(verColLayer);
    map.add(_depColLayer);

    global.getFeatureLayerView = function () {
      let featureLayerView = null;
      mapView.map.layers.forEach(function (layer, index) {
        mapView.whenLayerView(layer).then(function (layerView) {
          if (layer.type === "feature") {
            featureLayerView = layerView;
          }
        })
          .catch(console.error);
      });
      return featureLayerView;
    }

    mapView.ui.add("infoDiv", "top-right");
    mapView.ui.add("verVeredasBtn", "top-right");
    mapView.ui.add("verVeredasLayerBtn", "top-rigth");
    mapView.ui.add(homeBtn, "top-right");

    // Diálogo para los departamentos

    deptDialog = new Dialog({
      title: "Usuarios",
      content: "<div id='saludo'>Cargando...</div>",
      style: "width: 50%;",
    });

    verDialog = new Dialog({
      title: "Veredas",
      content: "<div id='tablaVeredas'>Cargando...</div>",
      style: "width: 70%;",
    });

    verColLayer.queryFeatures({
      where: "1=1",
      returnGeometry: false,
      outFields: ["*"]
    }).then(function (results) {
      veredas = results.features;
    });

    global.buscarVereda = function (nomVer) {
      verColLayer.queryFeatures({
        where: `NOMBRE_VER='${nomVer.toUpperCase()}'`,
        returnGeometry: false,
        outFields: ["*"]
      }).then(function (results) {
        cargarVeredas(results.features, 1);
      });
    };


    depColLayer.popupTemplate = deptPopup;
    _depColLayer.popupTemplate = deptPopup;

    global.verVereda = function (nomVer) {
      verColLayer.definitionExpression = `NOMBRE_VER='${nomVer.toUpperCase()}'`;
      verDialog.hide();
      verColLayer.queryFeatures({
        where: `NOMBRE_VER='${nomVer.toUpperCase()}'`,
        returnGeometry: true,
        outFields: ["*"]
      }).then(function (results) {
        mapView.popup.title = results.features[0].attributes.NOMBRE_VER;
        mapView.popup.open({
          location: {
            latitude: results.features[0].geometry.centroid.latitude,
            longitude: results.features[0].geometry.centroid.longitude
          },
          title: "Información de " + results.features[0].attributes.NOMBRE_VER,
          content: `
            OBJECTID: ${results.features[0].attributes.OBJECTID} <br> 
            Código DANE departamento: ${results.features[0].attributes.DPTO_CCDGO} <br> 
            Año de creación del departamento: ${results.features[0].attributes.DPTO_NANO_CREACION} <br> 
            Nombre del departamento: ${results.features[0].attributes.DPTO_CNMBRE} <br> 
            Acto administrativo de creación del departamento: ${results.features[0].attributes.DPTO_CACTO_ADMNSTRTVO} <br> 
            Área oficial del departamento en Km2: ${results.features[0].attributes.DPTO_NAREA} <br> 
            Año vigencia de información municipal (Fuente DANE): ${results.features[0].attributes.DPTO_NANO} <br> 
            `
        });
        mapView.extent = results.features[0].geometry.extent.expand(1.5);
        verColLayer.opacity = .75;
      });
    };

  });
