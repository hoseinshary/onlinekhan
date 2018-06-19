<template>
    <section v-bind:class="cssClass" style="padding:6px">
        <div v-bind:class="'card box ' + variant">
            <div class="card-header">
                <div class="col-xs-12 text-left font-box-text">
                    {{ title }}
                </div>
            </div>

            <div class="card-body">
              <div class="text-center">
                <div class="font-box-num">{{counter}}</div>
              </div>
            </div>
            <div class="card-footer text-muted">
                <a >
                    <div class="float-right" @click="openDetail()">جزییات</div>
                    <div class="float-left">
                        <i class="fas fa-arrow-left fa-lg"></i>
                    </div>
                </a>
            </div>
        </div>
    </section>
</template>

<script>
export default {
  data() {
    return {};
  },
  //   props: ["title", "counter"]
  props: {
    title: {
      type: String,
      required: true
    },
    counter: {
      type: Number,
      default: 0
    },
    cssClass: {
      type: String,
      default: "col-sm-6 col-md-3"
    },
    variant: {
      type: String,
      default: "box-blue1"
    }
  },
  methods:{
    openDetail() {
      this.$emit('detailsEvent');
    }
  }
};
</script>

<style lang="scss">
$colorWhite:#fff;

.box {
  border: 3px solid #e4e4e4;
}
.box.card {
 font-size: medium;
}
.box .card-header {
  border-bottom-color: $colorWhite;
  border-bottom-width: 1px;
}

.box .card-body,
.box .card-header {
  color: $colorWhite;
  padding-bottom: 10px;
  padding-left: 10px;
  padding-right: 10px;
  padding-top: 4px;
}

.box .card-body .text-center {
  border-radius: 20px;
  padding-bottom: 10px;
  padding-top: 10px;
}

.box .card-footer a {
  cursor: pointer;
  color: $colorWhite;
}

.box .card-footer {
  padding: 0px 6px 6px 6px;
  height: 25px;
}
.font-box-num{
  font-size: x-large;
}
/*---------box-red1----------*/
$colorRed1:#ff5515;

.box.box-red1 .card-body,
.box.box-red1 .card-header {
  background-color: #ff6e24;
}

.box.box-red1 .card-body .text-center {
  background-color: $colorRed1;
}

.box.box-red1 .card-footer {
  background-color: $colorRed1;
  border-color: $colorRed1;
}

/*---------box-red2----------*/
$colorRed2:#f14200;

.box.box-red2 .card-body,
.box.box-red2 .card-header {
  background-color: #ff6024;
}

.box.box-red2 .card-body .text-center {
  background-color: $colorRed2;
}

.box.box-red2 .card-footer {
  background-color: $colorRed2;
  border-color: $colorRed2;
}

/*---------box-red3----------*/
$colorRed3:#e83f00;

.box.box-red3 .card-body,
.box-red3 .card-header {
  background-color: #ff4c1b;
}

.box.box-red3 .card-body .text-center {
  background-color: $colorRed3;
}

.box.box-red3 .card-footer {
  background-color: $colorRed3;
  border-color: $colorRed3;
}

/*---------box-yellow1----------*/
$colorYellow1:#eec00a;

.box.box-yellow1 .card-header {
  color: #856a00;
}

.box.box-yellow1 .card-body,
.box.box-yellow1 .card-header {
  background-color: #fcf346;
}

.box.box-yellow1 .card-body .text-center {
  background-color: $colorYellow1;
  color: $colorWhite;
}

.box.box-yellow1 .card-footer {
  background-color: $colorYellow1;
  border-color: $colorYellow1;
}

/*---------box-yellow2----------*/
$colorYellow2:#e4b912;

.box.box-yellow2 .card-header {
  color: #856a00;
}

.box.box-yellow2 .card-body,
.box.box-yellow2 .card-header {
  background-color: #fdff11;
}

.box.box-yellow2 .card-body .text-center {
  background-color: $colorYellow2;
  color: $colorWhite;
}

.box.box-yellow2 .card-footer {
  background-color: $colorYellow2;
  border-color: $colorYellow2;
}

/*---------box-yellow3----------*/
$colorYellow3:#c6a31a;

.box.box-yellow3 .card-header {
  color: #856a00;
}

.box.box-yellow3 .card-body,
.box.box-yellow3 .card-header {
  background-color: #ffe200;
}

.box.box-yellow3 .card-body .text-center {
  background-color: $colorYellow3;
  color: $colorWhite;
}

.box.box-yellow3 .card-footer {
  background-color: $colorYellow3;
  border-color: $colorYellow3;
}

/*---------box-blue1----------*/
$colorBlue1:#1d9de8;

.box.box-blue1 .card-body,
.box-blue1 .card-header {
  background-color: #26c2fe;
}

.box.box-blue1 .card-body .text-center {
  background-color: $colorBlue1;
}

.box.box-blue1 .card-footer {
  background-color: $colorBlue1;
  border-color: $colorBlue1;
}

/*---------box-blue2----------*/
$colorBlue2:#038cdc;

.box.box-blue2 .card-body,
.box-blue2 .card-header {
  background-color: #0bafed;
}

.box.box-blue2 .card-body .text-center {
  background-color: $colorBlue2;
}

.box.box-blue2 .card-footer {
  background-color: $colorBlue2;
  border-color: $colorBlue2;
}

/*---------box-blue3----------*/
$colorBlue3:#0077f8;

.box.box-blue3 .card-body,
.box-blue3 .card-header {
  background-color: #0097ef;
}

.box.box-blue3 .card-body .text-center {
  background-color: $colorBlue3;
}

.box.box-blue3 .card-footer {
  background-color: $colorBlue3;
  border-color: $colorBlue3;
}

/*---------box-aqua----------*/
$colorAqua:#36c1c1;

.box.box-aqua .card-body,
.box-aqua .card-header {
  background-color: #39cccc;
}

.box.box-aqua .card-body .text-center {
  background-color: $colorAqua;
}

.box.box-aqua .card-footer {
  background-color: $colorAqua;
  border-color: $colorAqua;
  color: #cceef7;
}

/*--------green--------------*/
/*---------box-green1----------*/
$colorGreen1:#39e000;

.box.box-green1 .card-body,
.box-green1 .card-header {
  background-color: #60ff00;
}

.box.box-green1 .card-body .text-center {
  background-color: $colorGreen1;
}

.box.box-green1 .card-footer {
  background-color: $colorGreen1;
  border-color: $colorGreen1;
}

/*---------box-green2----------*/
$colorGreen2:#3eb42c;

.box.box-green2 .card-body,
.box-green2 .card-header {
  background-color: #60ff00;
}

.box.box-green2 .card-body .text-center {
  background-color: $colorGreen2;
}

.box.box-green2 .card-footer {
  background-color: $colorGreen2;
  border-color: $colorGreen2;
}

/*---------box-green3----------*/
$colorGreen3:#00952c;

.box.box-green3 .card-body,
.box-green3 .card-header {
  background-color: #00aa32;
}

.box.box-green3 .card-body .text-center {
  background-color: $colorGreen3;
}

.box.box-green3 .card-footer {
  background-color: $colorGreen3;
  border-color: $colorGreen3;
}
</style>
