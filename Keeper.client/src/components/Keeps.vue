<template>
  <div class="row row-cols-1 row-cols-md-3 g-4">
    <div class="col">
      <div class="card h-100">
        <img :src="keep.img" class="card-img-top" alt="..." />
        <div class="card-body">
          <h5 class="card-title">{{ keep.name }}</h5>
        </div>
      </div>
    </div>
  </div>
</template>






<script>
export default {
  props: {
    keep: {
      type: Object,
      required: true,
    },
  },
  setup(props) {
    return {

    };
  },
};
</script>