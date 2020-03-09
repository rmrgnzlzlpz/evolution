var ListadoVeredas = {};

/**
 * Departamentos
 */

var departmentLabel = {
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

var popupDepartments = {
  "title": "{DPTO_CNMBRE}",
  "content": "<b>Codigo:</b> {DPTO_CCDGO}<br><b>Año de Creación:</b> {DPTO_NANO_CREACION}<br><b>Area:</b> {DPTO_NAREA} Km2",
}

var styleDepartment = {
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

var renderDepartment = {
  type: "simple",
  symbol: {
    type: "simple-fill",
    color: "cyan",
    style: "solid",
    outline: {
      color: "grey",
      width: 1
    }
  }
}

/**
 * Veredas
 */

var PopupVereda = {
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

require([
  "esri/Map",
  "esri/views/MapView",
  "esri/layers/FeatureLayer",
  "esri/views/ui/DefaultUI",
  "esri/widgets/BasemapToggle",
  "esri/widgets/Search",
  "esri/layers/GraphicsLayer",
  "esri/Graphic",
  "esri/widgets/Sketch",
  "esri/widgets/Sketch/SketchViewModel",
  "esri/widgets/Popup"
], function (Map, MapView, FeatureLayer, DefaultUI, BasemapToggle, Search, GraphicsLayer, Graphic, Sketch, SketchViewModel, Popup) {
  let graphicsLayer = new GraphicsLayer();

  var map = new Map({
    basemap: "streets"
  });

  var view = new MapView({
    container: "viewDiv",
    map: map,
    zoom: 6,
    center: [-72.4782449846235, 4.887407292289377],
  });

  /**
   * Loading Departmentos
   */
  var FeatureDepartamento = new FeatureLayer({
    url: "https://ags.esri.co/server/rest/services/DA_DANE/departamento_mgn2016/MapServer",
    outFields: ["*"],
    popupTemplate: popupDepartments,
    opacity: .3,
    renderer: {
      type: "simple",
      symbol: {
        type: "simple-fill",
        color: "grey",
        style: "solid",
        outline: {
          color: "black",
          width: 2
        }
      }
    },
    labelingInfo: [styleDepartment]
  });

  /**
   * Loaging Veredas
   */
  var FeatureVereda = new FeatureLayer({
    url: "https://ags.esri.co/server/rest/services/DA_DatosAbiertos/VeredasColombia/MapServer/0",
    outFields: ["*"],
    opacity: 1,
    renderer: {
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
    },
    popupTemplate: PopupVereda
  });

  var sketchViewModel = new SketchViewModel({
    layer: graphicsLayer,
    view: view,
    polygonSymbol: {
      type: "simple-fill",
      style: "none",
      outline: {
        color: "black",
        width: 1
      }
    }
  });

  // Defines an action to zoom out from the selected feature
  var zoomOutAction = {
    // This text is displayed as a tooltip
    title: "Zoom out",
    // The ID by which to reference the action in the event handler
    id: "zoom-out",
    // Sets the icon font used to style the action button
    className: "esri-icon-zoom-out-magnifying-glass"
  };
  // Adds the custom action to the popup.
  view.popup.actions.push(zoomOutAction);

  // The function to execute when the zoom-out action is clicked
  function zoomOut() {
    // in this case the view zooms out two LODs on each click
    view.goTo({
      center: view.center,
      zoom: view.zoom - 2
    });
  }

  // This event fires for each click on any action
  view.popup.on("trigger-action", function (event) {
    // If the zoom-out action is clicked, fire the zoomOut() function
    if (event.action.id === "zoom-out") {
      zoomOut();
    }
  });

  let sketch = new Sketch({
    layer: graphicsLayer,
    view: view,
    viewModel: sketchViewModel,
    creationMode: "update"
  });

  // Listen to sketch widget's create event.
  sketch.on("create", function (event) {
    console.log("Envento de Sketch: ", event.state);
    if (event.state === "complete") {
      if (FeatureDepartamento) {
        var query = FeatureDepartamento.createQuery();
        query.geometry = event.graphic.geometry;
        query.distance = 2;
        query.units = "miles";
        query.spatialRelationship = "intersects";  // this is the default
        query.returnGeometry = true;
        query.outFields = ["*"];
        console.log("Evento", event);

        FeatureDepartamento.queryFeatures(query)
          .then(function (response) {
            view.when(function () {
              console.log(response.features);
              stateMap = true;
              view.extent = response.features[0].geometry.extent;
              view.popup.title = response.features[0].attributes.DPTO_CNMBRE;
              view.popup.open({
                location: {
                  latitude: response.features[0].geometry.centroid.latitude,
                  longitude: response.features[0].geometry.centroid.longitude
                },
                title: response.features[0].attributes.DPTO_CNMBRE,
                content: `
                  OBJECTID: ${response.features[0].attributes.OBJECTID} <br> 
                  Código DANE departamento: ${response.features[0].attributes.DPTO_CCDGO} <br> 
                  Año de creación del departamento: ${response.features[0].attributes.DPTO_NANO_CREACION} <br> 
                  Nombre del departamento: ${response.features[0].attributes.DPTO_CNMBRE} <br> 
                  Acto administrativo de creación del departamento: ${response.features[0].attributes.DPTO_CACTO_ADMNSTRTVO} <br> 
                  Área oficial del departamento en Km2: ${response.features[0].attributes.DPTO_NAREA} <br> 
                  Año vigencia de información municipal (Fuente DANE): ${response.features[0].attributes.DPTO_NANO} <br> 
                `
              });
              FeatureVereda.definitionExpression = `COD_DPTO=${response.features[0].attributes.DPTO_CCDGO}`;
              FeatureVereda.opacity = .75;
              FeatureVereda.renderer = renderDepartment;
              view.goTo(response.features[0].geometry.extent.expand(2));
            });

          });

      }
    }
  });

  sketch.on("delete", function (event) {
    FeatureVereda.definitionExpression = `1=0`;
    FeatureVereda.opacity = 1;
  });

  var toggle = new BasemapToggle({
    view: view,
    nextBasemap: "hybrid"
  });

  // var searchWidget = new Search({ view: view });

  view.ui.add(sketch, "top-right");
  // view.ui.add(searchWidget, "bottom-left");
  view.ui.add(toggle, "bottom-right");
  view.ui.add("instruction", "top-left");
  map.add(FeatureDepartamento);

  FeatureVereda.definitionExpression = `COD_DPTO=0`;
  ListadoVeredas = FeatureVereda;
  map.add(FeatureVereda);
});



function ListarVeredas(veredas, pagina) {
  if (pagina > 0) {
    var tabla = `
      <table class="table table-striped">
        <thead>
          <tr>
            <th scope="col">#</th>
            <th scope="col">Código</th>
            <th scope="col">Nombre</th>
            <th scope="col">Departamento</th>
            <th scope="col">Municipio</th>
            <th scope="col">Detalles</th>
          </tr>
        </thead>
      <tbody>`;
    var row = "";
    for (var i = 0; i < (veredas.length < 10 ? veredas.length : 10); i++) {
      row += `
        <tr>
          <th scope="row">${i + (10 * (pagina - 1))}</th>
          <th>${veredas[i].attributes.CODIGO_VER}</th>
          <th>${veredas[i].attributes.NOMBRE_VER}</th>
          <th>${veredas[i].attributes.NOM_DEP}</th>
          <th>${veredas[i].attributes.NOMB_MPIO}</th>
          <th><a class="btn btn-primary" title="Presiona Click" onclick="global.verVereda('${veredas[i].attributes.NOMBRE_VER}')">🔍</a></th>
        </tr>`
    }
    tabla += d;

    tabla += `
        </tbody>
      </table>
      <ul class="pagination justify-content-centerx|">
      <li class="page-item ${pagina === 1 ? "disabled" : ""}">
          <a class="btn btn-primary btn-lg active" onclick="ListarVeredas(ListadoVeredas.slice(${(pagina - 2) * 10}, ${(pagina - 2) * 10 + 10}), ${pagina - 1})">Anterior</a>
      </li>
      <li class="page-item ${Math.ceil(ListadoVeredas.length / 10) === pagina ? "disabled" : ""}">
          <a class="btn btn-primary btn-lg active" onclick="ListarVeredas(ListadoVeredas.slice(${pagina * 10}, ${pagina * 10 + 10}), ${pagina + 1})">Siguiente</a>
      </li>
      </ul>
    `;
    $("#tablaVeredas").html(tabla);
  }
}

function verificarVer() {
  if (ListadoVeredas === undefined) {
    alert("Loading...");
  } else {
    ListarVeredas(ListadoVeredas.slice(0, 10), 1);
  }
  verDialog.show();
}