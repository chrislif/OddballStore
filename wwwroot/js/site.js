$(document).ready(() => {
	const container = document.querySelector('.container');

	container.addEventListener('mouseover', (e) => {
		const eye = document.querySelectorAll('.eyes');
		[].forEach.call(eye, function (eye) {
			let mouseX = eye.getBoundingClientRect().right;
			if (eye.classList.contains('eyes')) {
				mouseX = eye.getBoundingClientRect().left;
			}
			let mouseY = eye.getBoundingClientRect().top;
			let radianDegrees = Math.atan2(e.pageX - mouseX, e.pageY - mouseY);
			let rotationDegrees = radianDegrees * (180 / Math.PI) * -1 + 180;
			eye.style.transform = `rotate(${rotationDegrees}deg)`;
		});
	});
});
