<template>
  <b-spinner
    small
    v-if="visible"
  ></b-spinner>
</template>

<script>
export default {
  props: {
		visible: {
			type: Boolean,
			default: true
		}
	},
}
</script>
<style>
</style>>
 