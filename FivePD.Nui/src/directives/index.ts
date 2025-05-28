import { type Directive } from "vue";

const clickOutside: Directive = {
  mounted: (element: any, binding: any) => {
    element.onClickOutside = document.body.addEventListener('click', (e) => {
      if (element === e.target || element.contains(e.target) || [...element.children].some((x: any) => x.contains(e.target))) return;
      if (binding?.value) binding.value();
    })
  },
  unmounted: (element: any) => {
    document.body.removeEventListener('click', element.onClickOutside)
  }
}

const directives = {
  clickOutside
};
export default directives;