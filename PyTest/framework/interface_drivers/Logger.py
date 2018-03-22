import logging

class Logger:

    @staticmethod
    def add_log(log_file_name='out', class_name='root', log_level=3, log_type='info', message=''):
        log_format = '%(levelname)s - %(message)s'
        if log_level >= 2:
            log_format = '%(name)s - {}'.format(log_format)
        if log_level == 3:
            log_format = '%(asctime)s - {}'.format(log_format)

        logger = logging.getLogger(class_name)
        logger.setLevel(logging.INFO)

        fh = logging.FileHandler('{}.log'.format(log_file_name))
        formatter = logging.Formatter(log_format)
        fh.setFormatter(formatter)
        logger.handlers.clear()
        logger.addHandler(fh)

        if log_type == 'info':
            logger.info(message)
        elif log_type == 'warn':
            logger.warning(message)
        elif log_type == 'error':
            logger.error(message)
